using System;
using System.Security.Claims;
using System.Text.Json;
using Matario.Application.Constants;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Enums.AuthenticationModule;
using static Matario.Application.Constants.ApplicationConstants;

namespace Matario.Application.Services.AuthenticationModule
{
	public class ManageJwtService : IManageJwtService
	{
		private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        
        public ManageJwtService(IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IPermissionRepository permissionRepository)
        {
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            
        }
        public async Task<AccessTokenDTO> GenerateAccessToken(User user)
        {
            // getting access token duration
            const int accessTokenDuration = TimeConstants.MinutesInAnHour * TimeConstants.HoursInADay;
            var accessTokenExpirationTime = DateAndTimeUtilities.AddMinutes(accessTokenDuration);

            // get all roleIds
            List<long> roleIds = new ();
            List<string> roleNames = new ();
            user.Roles.ForEach(role =>
            {
                roleNames.Add(role.Name.ToLower()); // to lower helps to maintain case insensitive search later on
                roleIds.Add(role.Id);
            });

            // get the permissions for each role user has
            IEnumerable<string> permissionNames = (await _permissionRepository.GetPermissionsForRoles(roleIds))
                .Select(permission => permission.Name.ToLower()); // to lower helps to maintain case insensitive search later on

            // Add roles and permissions of user to claims
            string permissionsClaim = JsonSerializer.Serialize(permissionNames);
            string rolesClaim = JsonSerializer.Serialize(roleNames);
            
            var accessTokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim(ClaimConstants.IdClaimType, user.Id.ToString()),
                new Claim(ClaimConstants.RolesClaimType, rolesClaim),
                new Claim(ClaimConstants.PermissionsClaimType, permissionsClaim)
            };
            string generatedToken = _jwtService.GenerateToken(accessTokenClaims, accessTokenExpirationTime);

            return new AccessTokenDTO
            {
                AccessToken = generatedToken,
                AccessTokenExpirationTime = accessTokenExpirationTime
            };
        }

        public async Task<RefreshToken> GenerateRefreshTokenForUserAsync(User user)
        {

            const int daysInAMonth = 30;

            const int refreshTokenDuration = TimeConstants.MinutesInAnHour * TimeConstants.HoursInADay * daysInAMonth;
            var refreshTokenExpirationTime = DateAndTimeUtilities.AddMinutes(refreshTokenDuration);


            var refreshTokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var generatedToken = _jwtService.GenerateToken(refreshTokenClaims, refreshTokenExpirationTime);
            
            var refreshTokenFromDb = await _refreshTokenRepository.FirstOrDefaultAsync(token => token.UserId == user.Id);
            if (refreshTokenFromDb is null)
            {
                refreshTokenFromDb = new RefreshToken
                {
                    UserId = user.Id
                };
                await _refreshTokenRepository.AddAsync(refreshTokenFromDb);
                await _unitOfWork.SaveChangesAsync();
            }

            refreshTokenFromDb.ExpirationTime = refreshTokenExpirationTime;
            refreshTokenFromDb.Revoked = false;
            refreshTokenFromDb.Token = generatedToken;
            refreshTokenFromDb.UseageCount = 0;

            await _refreshTokenRepository.UpdateAsync(refreshTokenFromDb);
            await _unitOfWork.SaveChangesAsync();

            return refreshTokenFromDb;
        }


        public async Task<AuthenticationResponse> GenerateAccessAndRefreshToken(User user)
        {
            AccessTokenDTO accessToken= await GenerateAccessToken(user);
            RefreshToken refreshToken = await GenerateRefreshTokenForUserAsync(user);

            return new AuthenticationResponse
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpirationTime = accessToken.AccessTokenExpirationTime,
                RefreshTokenExpirationTime = refreshToken.ExpirationTime
            };
        }

        public async Task<IEnumerable<Claim>> DecryptToken(string token)
        {
            await Task.CompletedTask;
            // check if token has expired.
            bool isValidToken = _jwtService.IsValid(token);
            // if yes, throw exception
            if (!isValidToken)
            {
                throw new UnAuthorizedException("Invalid token");
            }
            // Decrypt the token
            IEnumerable<Claim> claims = _jwtService.DecryptToken(token);

            return claims;
        }
        public string? GetClaimValue(IEnumerable<Claim> claims, string claimType)
        {
            return claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
        }
        public async Task<bool> IsSuperAdmin(string token)
        {
            
                IEnumerable<Claim> claims = await DecryptToken(token);
                return IsSuperAdmin(claims);
            
        }

        public  bool IsSuperAdmin(IEnumerable<Claim> claims)
        {
            try
            {
                var claimValue = GetClaimValue(claims, ClaimTypes.Role);
                return UserRole.SuperAdmin.ToString().Equals(claimValue);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UserHasPermission(string token, string permissons = "")
        {
            // decrypt token
            IEnumerable<Claim> claims = await DecryptToken(token);

            if (IsSuperAdmin(claims)) return true;

            string? permissionsClaimAsString = GetClaimValue(claims, ClaimConstants.PermissionsClaimType);
            // get permissions claim
            IEnumerable<string> userPermissionsSet = new HashSet<string>();
            if (permissionsClaimAsString is null) return false;

            try
            {
                // user permission saved as lower case during jwt encryption
                userPermissionsSet = JsonSerializer.Deserialize<HashSet<string>>(permissionsClaimAsString) ?? new HashSet<string>();
            }
            catch(Exception)
            {

            }

            // deserialize permissions
            IEnumerable<string> requiredPermissions = permissons.Split(",").Select(requiredPermission => requiredPermission.Trim().ToLower());
            
            // filter to see if user has at least one of the permissions
            foreach(string requiredPermission in requiredPermissions)
            {
                bool userHasPermission = userPermissionsSet.Contains(requiredPermission);
                if (userHasPermission)
                {
                    return userHasPermission;
                }
            }

            return false;
        }
    }
}

