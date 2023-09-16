using System;
using System.Security.Claims;

namespace Matario.Application.Contracts.Services.AuthenticationServiceModule
{
	public interface IJwtService
	{
        string GenerateToken(IEnumerable<Claim> claims, int durationInMinutes);
    }
}

