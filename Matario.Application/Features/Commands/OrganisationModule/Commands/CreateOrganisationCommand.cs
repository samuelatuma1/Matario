using System;
using Matario.Domain.Entities.OrganisationModule;
using MediatR;

namespace Matario.Application.Features.Commands.OrganisationModule.Commands
{
	public record CreateOrganisationCommand(string Name = "", string LogoUrl = "", string Description = "") : IRequest<Organisation>
	{
    }
}

