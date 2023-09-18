using System;
using System.Security.Claims;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Services.AuthenticationModule
{
	public class ManageJwtService : IManageJwtService
	{
		private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public ManageJwtService(IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public AccessTokenDTO GenerateAccessToken(User user)
        {
            const int minutesInAnHour = 60;
            const int hoursInADay = 24;

            const int accessTokenDuration = minutesInAnHour * hoursInADay;
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
            DateTime timeNow = DateAndTimeUtilities.Now();
            var refreshTokenExpirationTime = DateAndTimeUtilities.AddMinutes(refreshTokenDuration);
            RefreshToken? refreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(token => token.UserId == user.Id && token.Revoked == false && token.ExpirationTime > timeNow);
            if(refreshToken is not null)
            {
                return refreshToken;
            }

            var refreshTokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var generatedToken = _jwtService.GenerateToken(refreshTokenClaims, refreshTokenExpirationTime);
            refreshToken = new RefreshToken
            {
                ExpirationTime = refreshTokenExpirationTime,
                Revoked = false,
                UseageCount = 0,
                UserId = user.Id,
                Token = generatedToken
            };

            refreshToken = await _refreshTokenRepository.AddAsync(refreshToken);

            return refreshToken;
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
    }
}

