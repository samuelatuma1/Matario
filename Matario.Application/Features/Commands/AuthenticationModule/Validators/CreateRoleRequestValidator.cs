using System;
using FluentValidation;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;

namespace Matario.Application.Features.Commands.AuthenticationModule.Validators
{
	public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
	{
		private readonly IRoleRepository _roleRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        public CreateRoleRequestValidator(IRoleRepository roleRepository, IAuthenticationRepository authenticationRepository)
        {
            _roleRepository = roleRepository;
            _authenticationRepository = authenticationRepository;

            RuleFor(role => role.Name).MustAsync(IsUniqueRoleName);  // We don't want roles repeating now do we
            RuleFor(role => role.CreatedBy).MustAsync(IsSuperAdmin); // Must be a super admin to create roles
        }

        private async Task<bool> IsUniqueRoleName(string roleName, CancellationToken token = default)
        {
            return await _roleRepository.IsUniqueRoleName(roleName);
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

