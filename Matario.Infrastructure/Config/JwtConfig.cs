using System;
namespace Matario.Application.Config
{
	public class JwtConfig
	{
		public string SecretKey { get; set; } = string.Empty;

        public JwtConfig()
		{
		}
	}
}

