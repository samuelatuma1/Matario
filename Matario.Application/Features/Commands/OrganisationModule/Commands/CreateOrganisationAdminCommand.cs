using System;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Domain.Enums.AuthenticationModule;
using Matario.Application.Constants;

namespace Matario.Application.Features.Commands.OrganisationModule.Commands
{
	public class CreateOrganisationAdminCommand
	{
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = ApplicationConstants.AuthenticationConstants.DefaultUserPassword;

        public string OrganisationName { get; set; } = string.Empty;
    }
}

