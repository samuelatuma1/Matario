using System;
using FluentValidation;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;

namespace Matario.Application.Features.Commands.AuthenticationModule.Validators
{
	public class SignupRequestValidator : AbstractValidator<SignupRequest>
	{
		public SignupRequestValidator()
		{
			RuleFor(request => request.Email).EmailAddress();
			RuleFor(request => request.Password).MinimumLength(6);
		}
	}
}

