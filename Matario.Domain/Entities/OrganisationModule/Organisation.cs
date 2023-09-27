using System;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.OrganisationModule
{
	public class Organisation : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string LogoUrl { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public List<User> Users { get; set; } = new ();
    }
}

