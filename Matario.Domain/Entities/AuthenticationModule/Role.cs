using System;
using System.Text.Json.Serialization;
using Matario.Domain.Entities.Common;

namespace Matario.Domain.Entities.AuthenticationModule
{
	public class Permission : BaseEntity<long>
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public long? CreatedBy { get; set; }

		[JsonIgnore]
		public List<Role> Roles { get; set; } = new();
    }
}

