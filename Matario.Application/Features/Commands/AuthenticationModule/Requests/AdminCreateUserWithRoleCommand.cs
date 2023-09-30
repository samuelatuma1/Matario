using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record AdminCreateUserWithRoleCommand : IRequest<User?>
    {
        public string OrganisationName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public AdminCreateUserWithRoleCommand()
        {
        }

        public AdminCreateUserWithRoleCommand(string organisationName, string email, string password, string roleName)
        {
            OrganisationName = organisationName;
            Email = email;
            Password = password;
            RoleName = roleName;
        }


    }
}

