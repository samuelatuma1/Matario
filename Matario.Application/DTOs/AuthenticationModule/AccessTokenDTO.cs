using System;
namespace Matario.Application.DTOs.AuthenticationModule
{
	public class AccessTokenDTO
	{
		public string AccessToken { get; set; } = string.Empty;
		public DateTime AccessTokenExpirationTime { get; set; }
    }
}

