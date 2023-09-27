using System;
using FluentValidation;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Application.Features.Commands.OrganisationModule.Commands;

namespace Matario.Application.Features.Commands.OrganisationModule.Validators
{
	public class CreateOrganisationValidator : AbstractValidator<CreateOrganisationCommand>
	{
        private readonly IOrganisationRepository _organisationRepository;
        public CreateOrganisationValidator(IOrganisationRepository organisationRepository)
        {
            _organisationRepository = organisationRepository;
            RuleFor(o => o.Name).NotEmpty()
                .MustAsync(MustBeUnique)
                .WithMessage("Name of organisation must be unique");
        }

        private async Task<bool> MustBeUnique(string name, CancellationToken token = default)
        {
            return await _organisationRepository.IsUniqueName(name);
        }
    }
}

