using System;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record DeleteRoleRequest(long Id) : IRequest<Unit>
	{
	}
}

