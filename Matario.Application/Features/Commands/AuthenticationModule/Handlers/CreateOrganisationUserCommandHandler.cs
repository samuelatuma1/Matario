using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class CreateOrganisationUserCommandHandler : IRequestHandler<CreateOrganisationUserCommand, User?>
	{
        private readonly IUserService _userService;
        public CreateOrganisationUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> Handle(CreateOrganisationUserCommand request, CancellationToken cancellationToken)
        {
            var organisationUser = new OrganisationUserDTO(Email: request.Email, Password : request.Password, OrganisationName : request.OrganisationName);

            // Valdiation already happens in CreateOrganisationUser so no need to validate request
            await _userService.CreateOrganisationUser(organisationUser, cancellationToken);

            return await _userService.GetUserByEmail(request.Email);
        }
    }
}

