using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record CreateOrganisationUserCommand(string Email, string Password, string OrganisationName) : IRequest<User?>
    {
		
	}
}

