using System;
using Matario.Domain.Entities.Common;
using Matario.Domain.Entities.OrganisationModule;
using Matario.Domain.Enums.AuthenticationModule;

namespace Matario.Domain.Entities.AuthenticationModule
{
	public class User : BaseEntity<long>
	{
		public string Email { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public UserRole UserRole { get; set; }

		public List<Role> Roles { get; set; } = new ();

		public long? OrganisationId { get; set; }

		public Organisation? Organisation { get; set; }
    }
}

