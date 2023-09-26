using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Matario.Application.Config;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Utilities;
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
                signingCredentials: signingCredentials,
                expires: DateAndTimeUtilities.AddMinutes(durationInMinutes)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }

        public string GenerateToken(IEnumerable<Claim> claims, DateTime expiry)
        {
            var systemSymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var signingCredentials = new SigningCredentials(systemSymmetricKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: expiry
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }

        public IEnumerable<Claim> DecryptToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decryptedToken = handler.ReadJwtToken(token);

            return decryptedToken.Claims;
        }

        public bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return jwtSecurityToken.ValidTo > DateAndTimeUtilities.Now();
        }
    }
}

