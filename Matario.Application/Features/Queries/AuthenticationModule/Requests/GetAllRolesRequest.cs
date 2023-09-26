using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Queries.AuthenticationModule.Requests
{
	public record GetAllRolesRequest() : IRequest<IEnumerable<Role>>
	{
		
	}
}

