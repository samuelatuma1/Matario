using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Matario.Application.Config;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Matario.Infrastructure.Services.AuthenticationServiceModule
{
	public class JwtService : IJwtService
	{
        private readonly JwtConfig _jwtConfig;
		public JwtService(IOptions<JwtConfig> jwtConfig)
		{
            _jwtConfig = jwtConfig.Value;
		}

        public string GenerateToken(IEnumerable<Claim> claims, int durationInMinutes)
        {
            var systemSymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var signingCredentials = new SigningCredentials(systemSymmetricKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }
    }
}

