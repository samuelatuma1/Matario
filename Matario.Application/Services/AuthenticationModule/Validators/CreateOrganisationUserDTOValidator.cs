using System;
using FluentValidation;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Application.DTOs.AuthenticationModule;

namespace Matario.Application.Services.AuthenticationModule.Validators
{
	public class CreateOrganisationUserDTOValidator : AbstractValidator<OrganisationUserDTO>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public CreateOrganisationUserDTOValidator(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
            RuleFor(request => request.Email).EmailAddress();
            RuleFor(request => request.Password).MinimumLength(6);
            RuleFor(request => request.Email).MustAsync(EmailIsUnique);
        }

        private async Task<bool> EmailIsUnique(string email, CancellationToken cancellationToken = default)
        {
            return await _authenticationRepository.IsUniqueEmail(email);
        }
    }
}

