using System;
using System.Security.Claims;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Features.Commands.AuthenticationModule.Common
{
	public static class ManageJwt
	{
		public static string GenerateToken(IJwtService jwtService, User user)
		{
			const int DURATION = 60 * 24 * 7;
			var claims = new List<Claim>()
			{
				new Claim("email", user.Email),
				new Claim("role", user.UserRole.ToString())
			};
			return jwtService.GenerateToken(claims, DURATION);
		}
	}
}

