using System;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Contracts.Services.AuthenticationServiceModule
{
	public interface IUserService
	{
		Task CreateUser(SignUpDTO signUpDTO, CancellationToken cancellation = default);
		Task CreateOrganisationUser(OrganisationUserDTO organisationUser, CancellationToken cancellationToken = default);
		Task<User?> GetUserByEmail(string email);

		Task CreateOrganisationUserWithRole(OrganisationUserWithRoleDTO organisationUserWithRoleDTO, CancellationToken cancellationToken = default);


    }

}

