using System;
namespace Matario.Application.DTOs.AuthenticationModule
{
	public class AuthenticationResponse
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime AccessTokenExpirationTime { get; set; }
		public DateTime RefreshTokenExpirationTime { get; set; }
	}
}

