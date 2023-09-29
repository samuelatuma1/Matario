using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class CreateOrganisationUserWithRoleCommandHandler : IRequestHandler<CreateOrganisationUserWithRoleCommand, User?>
	{
        private readonly IUserService _userService;
        public CreateOrganisationUserWithRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async  Task<User?> Handle(CreateOrganisationUserWithRoleCommand request, CancellationToken cancellationToken)
        {
            var organisationUserWithRole = new OrganisationUserWithRoleDTO(
                Email: request.Email,
                Password: request.Password,
                OrganisationName: request.OrganisationName,
                RoleName: request.RoleName
                );

            await _userService.CreateOrganisationUserWithRole(organisationUserWithRole, cancellationToken);

            return await _userService.GetUserByEmail(request.Email);
        }
    }
}

