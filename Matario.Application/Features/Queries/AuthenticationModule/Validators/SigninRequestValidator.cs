using System;
using FluentValidation;
using Matario.Application.Features.Queries.AuthenticationModule.Requests;

namespace Matario.Application.Features.Queries.AuthenticationModule.Validators
{
	public class SigninRequestValidator : AbstractValidator<SigninRequest>
	{
		public SigninRequestValidator()
		{
			RuleFor(request => request.Email).EmailAddress();
			RuleFor(request => request.Password).NotEmpty();
		}
	}
}

