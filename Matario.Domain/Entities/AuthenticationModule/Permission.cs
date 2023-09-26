using System;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.AuthenticationModule
{
	public class Role : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public long CreatedBy { get; set; }

		public List<User> Users { get; set; } = new();

		public List<Permission> Permissions { get; set; } = new();
    }
}

