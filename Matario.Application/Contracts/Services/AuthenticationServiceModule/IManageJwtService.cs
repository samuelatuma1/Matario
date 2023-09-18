using System;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.Services.AuthenticationServiceModule
{
	public interface IManageJwtService 
	{
        AccessTokenDTO GenerateAccessToken(User user);

        Task<RefreshToken> GenerateRefreshTokenForUserAsync(User user);

        Task<AuthenticationResponse> GenerateAccessAndRefreshToken(User user);
    }
}

