using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record CreateOrganisationUserWithRoleCommand(
		string Email,
		string Password,
		string OrganisationName,
		string RoleName) : IRequest<User?>
    {
	}
}

