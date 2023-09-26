using System;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
    public record CreatePermissionRequest(string Name, string Description = "", long CreatedBy = 0) : IRequest<Permission>
    {

    }
	
}

