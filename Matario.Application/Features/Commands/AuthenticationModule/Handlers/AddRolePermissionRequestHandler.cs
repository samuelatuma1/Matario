using System;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class AddRolePermissionRequestHandler : IRequestHandler<AddRolePermissionRequest, Role>
	{
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddRolePermissionRequestHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> Handle(AddRolePermissionRequest request, CancellationToken cancellationToken)
        {
            // get role
            Role role = await _roleRepository.GetRoleByName(request.RoleName) ??
                throw new NotFoundException("Role with name not found");
            // get permission
            Permission permission = await _permissionRepository.GetPermissionByName(request.PermissionName) ??
                throw new NotFoundException("Permission with name not found");

            bool roleHasPermission = role.Permissions.FirstOrDefault(rolePermission => rolePermission.Id.Equals(permission.Id)) is not null;
            if (!roleHasPermission)
            {
                role.Permissions.Add(permission);
                // permission.Roles.Add(role); // Kept for possible usecase. Please delete if none found
                await _unitOfWork.SaveChangesAsync();
            }

            return role;
        }
    }
}

