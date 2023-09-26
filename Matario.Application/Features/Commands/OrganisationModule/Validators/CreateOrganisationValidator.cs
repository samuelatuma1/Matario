using System;
using FluentValidation;
using Matario.Application.Features.Commands.OrganisationModule.Commands;

namespace Matario.Application.Features.Commands.OrganisationModule.Validators
{
	public class CreateOrganisationValidator : AbstractValidator<CreateOrganisationCommand>
	{
		public CreateOrganisationValidator()
		{
			RuleFor(o => o.Email).EmailAddress().NotEmpty();
			RuleFor(o => o.CreatedBy).NotEmpty();
		}
	}
}

