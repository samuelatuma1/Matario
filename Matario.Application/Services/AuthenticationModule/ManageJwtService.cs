using System;
using System.Security.Claims;
using Matario.Application.Constants;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Enums.AuthenticationModule;

namespace Matario.Application.Services.AuthenticationModule
{
	public class ManageJwtService : IManageJwtService
	{
		private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ManageJwtService(IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }
        public AccessTokenDTO GenerateAccessToken(User user)
        {
            const int accessTokenDuration = ApplicationConstants.TimeConstants.MinutesInAnHour * ApplicationConstants.TimeConstants.HoursInADay;
            var accessTokenExpirationTime = DateAndTimeUtilities.AddMinutes(accessTokenDuration);

            var accessTokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim("Id", user.Id.ToString())
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

            const int minutesInAnHour = 60;
            const int hoursInADay = 24;
            const int daysInAMonth = 30;

            const int refreshTokenDuration = minutesInAnHour * hoursInADay * daysInAMonth;
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
            AccessTokenDTO accessToken= GenerateAccessToken(user);
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
            try
            {
                IEnumerable<Claim> claims = await DecryptToken(token);
                var claimValue = GetClaimValue(claims, ClaimTypes.Role);
                return UserRole.SuperAdmin.ToString().Equals(claimValue);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

