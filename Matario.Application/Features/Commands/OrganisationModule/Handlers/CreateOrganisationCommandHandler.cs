using System;
using AutoMapper;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.OrganisationModule.Commands;
using Matario.Application.Features.Commands.OrganisationModule.Validators;
using Matario.Domain.Entities.OrganisationModule;
using MediatR;

namespace Matario.Application.Features.Commands.OrganisationModule.Handlers
{
	public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, Organisation>
	{
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrganisationCommandHandler(IOrganisationRepository organisationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _organisationRepository = organisationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Organisation> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            // validate CreateOrganisationDto
            var createorganisationValidator = new CreateOrganisationValidator(_organisationRepository);
            var validation = await createorganisationValidator.ValidateAsync(request, cancellationToken);

            if (validation.Errors.Any())
            {
                var errors = validation.Errors.ToDictionary(err => err.PropertyName, err => err.ErrorMessage);
                throw new ValidationException("Validation errors occured while creating organisation", errors);
            }

            // save organisation
            Organisation organisation = _mapper.Map<Organisation>(request);
            await _organisationRepository.AddAsync(organisation);
            await _unitOfWork.SaveChangesAsync();

            return organisation;
        }
    }
}

