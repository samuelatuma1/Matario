using System;
namespace Matario.Application.DTOs.AuthenticationModule
{
	public class HasPermissionAndOrganisationNameDTO
	{
		public bool HasPermission { get; set; }

		public string OrganisationName { get; set; } = string.Empty;
	}
}

