using System;
using FluentValidation;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;

namespace Matario.Application.Features.Commands.AuthenticationModule.Validators
{
	public class CreatePermissionRequestValidator : AbstractValidator<CreatePermissionRequest>
	{
		private readonly IPermissionRepository _permissionRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        public CreatePermissionRequestValidator(IPermissionRepository permissionRepository, IAuthenticationRepository authenticationRepository)
        {
            _permissionRepository = permissionRepository;
            _authenticationRepository = authenticationRepository;

            RuleFor(permission => permission.Name).MustAsync(IsUniquePermissionName);  // We don't want Permissions repeating now do we
            RuleFor(permission => permission.CreatedBy).MustAsync(IsSuperAdmin); // Must be a super admin to create Permissions
        }

        private async Task<bool> IsUniquePermissionName(string permissionName, CancellationToken token = default)
        {
            return await _permissionRepository.IsUniquePermissionName(permissionName);
        }

        private async Task<bool> IsSuperAdmin(long id, CancellationToken token = default)
        {
            // if id is 0, no need to check, not a valid id in our user table
            if(id == 0)
            {
                return false;
            }

            return await _authenticationRepository.IsSuperAdmin(id);
        }
    }
}

