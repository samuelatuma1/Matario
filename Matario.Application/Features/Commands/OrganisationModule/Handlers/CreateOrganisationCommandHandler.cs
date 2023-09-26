using System;
using Matario.Application.Features.Commands.OrganisationModule.Commands;
using Matario.Domain.Entities.OrganisationModule;
using MediatR;

namespace Matario.Application.Features.Commands.OrganisationModule.Handlers
{
	public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, Organisation>
	{
		public CreateOrganisationCommandHandler()
		{
		}

        public Task<Organisation> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            // validate CreateOrganisationDto

            // Verify Organisation is being created by a super admin
            // save organisation
            throw new NotImplementedException();
        }
    }
}

