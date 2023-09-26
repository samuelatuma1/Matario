using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
    public record CreateRoleRequest(string Name, string Description = "", long CreatedBy = 0) : IRequest<Role>
    {

    }
	
}

