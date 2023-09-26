using System;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.OrganisationModule
{
	public class Organisation : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Contact { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;
    }
}

