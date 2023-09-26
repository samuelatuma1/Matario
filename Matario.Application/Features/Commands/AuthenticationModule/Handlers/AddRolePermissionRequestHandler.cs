using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class AddRolePermissionRequestHandler : IRequestHandler<AddRolePermissionRequest, Role>
	{
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public AddRolePermissionRequestHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<Role> Handle(AddRolePermissionRequest request, CancellationToken cancellationToken)
        {
            // get role
            Role? role = await _roleRepository.GetRoleByName(request.RoleName);
            // get permission
            // check if role already has permission
            // if not
            // add permission to role
            // return role
            throw new NotImplementedException();
        }
    }
}

