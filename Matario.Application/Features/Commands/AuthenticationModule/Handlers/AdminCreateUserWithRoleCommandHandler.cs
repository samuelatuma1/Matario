using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;
using static Matario.Application.Constants.ApplicationConstants;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class AdminCreateUserWithRoleCommandHandler : IRequestHandler<AdminCreateUserWithRoleCommand, User?>
	{

        private readonly IUserService _userService;
        public AdminCreateUserWithRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> Handle(AdminCreateUserWithRoleCommand request, CancellationToken cancellationToken)
        {

            string roleName = request.RoleName.ToLower();

            // Validate that admin has priviledge to create user with the role he is trying to create 
            // using if because switch cannot handle logic in the case conditionals
            if (
                roleName == RoleConstants.Admin.ToLower() ||
                roleName == RoleConstants.Manager.ToLower()
                )
            {
                var organisationUserWithRoleDTO = new OrganisationUserWithRoleDTO(
                Email: request.Email,
                Password: request.Password,
                OrganisationName: request.OrganisationName,
                RoleName: request.RoleName
                );

                await _userService.CreateOrganisationUserWithRole(organisationUserWithRoleDTO, cancellationToken);

                return await _userService.GetUserByEmail(request.Email);
            }

            throw new UnAuthorizedException($"Not authorised to create user with role {roleName}");
            
                
            
        }
    }
}

