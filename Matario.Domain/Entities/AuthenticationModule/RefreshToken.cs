using System;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.AuthenticationModule
{
	public class RefreshToken : BaseEntity<long>
	{
		public string Token { get; set; } = string.Empty;

		public bool Revoked { get; set; }

		public long UserId { get; set; }

		public int UseageCount { get; set; }

		public DateTime ExpirationTime { get; set; }
	}
}

