using System;
namespace Matario.Application.DTOs.AuthenticationModule
{
	public record OrganisationUserDTO(string Email, string Password, string OrganisationName)
    {
	}
}

