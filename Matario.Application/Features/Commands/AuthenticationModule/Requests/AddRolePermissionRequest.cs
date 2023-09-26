using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record AddRolePermissionRequest(string RoleName, string PermissionName) : IRequest<Role>
	{

    }
}

