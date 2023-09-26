using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record UpdateRoleRequest(long Id, string? Name, string? Description) : IRequest<Role?>
	{
		
	}
}

