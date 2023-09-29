using System;
namespace Matario.Application.DTOs.AuthenticationModule
{
	public record OrganisationUserWithRoleDTO(string Email, string Password, string OrganisationName, string RoleName)
    {
		
	}
}

