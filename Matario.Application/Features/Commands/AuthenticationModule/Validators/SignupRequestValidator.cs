using System;
using FluentValidation;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;

namespace Matario.Application.Features.Commands.AuthenticationModule.Validators
{
	public class SignupRequestValidator : AbstractValidator<SignupRequest>
	{
		private readonly IAuthenticationRepository _authenticationRepository;
        public SignupRequestValidator(IAuthenticationRepository authenticationRepository)
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

