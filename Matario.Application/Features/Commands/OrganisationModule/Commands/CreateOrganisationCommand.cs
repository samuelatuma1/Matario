using System;
using Matario.Domain.Entities.OrganisationModule;
using MediatR;

namespace Matario.Application.Features.Commands.OrganisationModule.Commands
{
	public class CreateOrganisationCommand : IRequest<Organisation>
	{
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public long CreatedBy { get; set; }
    }
}

