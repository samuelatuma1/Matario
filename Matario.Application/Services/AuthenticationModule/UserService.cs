using System;
using Matario.Application.Config;
using System.Threading;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Contracts.UoW;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;
using Matario.Application.Services.AuthenticationModule.Validators;
using AutoMapper;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Microsoft.Extensions.Options;
using Matario.Application.Exceptions;
using Matario.Application.Contracts.DataAccess.OrganisationModule;
using Matario.Domain.Entities.OrganisationModule;

namespace Matario.Application.Services.AuthenticationModule
{
    /// <summary>
    /// Represents a service for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
	{
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authRepository;
        private readonly HashConfig _hashConfig;
        private readonly IManageJwtService _manageJwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IMapper mapper, IAuthenticationRepository authRepository, IOptions<HashConfig> hashConfig, IManageJwtService manageJwtService, IUnitOfWork unitOfWork, IOrganisationRepository organisationRepository, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _hashConfig = hashConfig.Value;
            _manageJwtService = manageJwtService;
            _unitOfWork = unitOfWork;
            _organisationRepository = organisationRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Creates a new user based on the provided signup information.
        /// </summary>
        /// <param name="signUpDTO">The DTO containing user signup information.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <exception cref="ValidationException">Thrown when input data fails validation.</exception>
        public async Task CreateUser(SignUpDTO signUpDTO, CancellationToken cancellation = default)
        {
            // validate sign up request
            var signupRequestValidator = new SignupDTOValidator(_authRepository);
            var signUpValidationResult = await signupRequestValidator.ValidateAsync(signUpDTO, cancellation);

            if (signUpValidationResult.Errors.Any())
            {
                IDictionary<string, string> errors = signUpValidationResult.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                throw new ValidationException("Validation errors", errors);
            }

            // convert signup request to entity
            User user = CreateUserWithHashedPassword(signUpDTO.Email, signUpDTO.Password);

            // save signup request in database
            await SaveUser(user);
        }

        /// <summary>
        /// Creates a new user associated with an organization.
        /// </summary>
        /// <param name="organisationUser">The DTO containing organization user signup information.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <exception cref="ValidationException">Thrown when input data fails validation.</exception>
        /// <exception cref="NotFoundException">Thrown when the specified organization is not found.</exception>
        public async Task CreateOrganisationUser(OrganisationUserDTO organisationUser, CancellationToken cancellationToken = default)
        {
            var user = await CreateBaseOrganisationUser(organisationUser, cancellationToken);
            // save user with organisation
            await SaveUser(user);
        }

        

        public async Task CreateOrganisationUserWithRole(OrganisationUserWithRoleDTO organisationUserWithRoleDTO, CancellationToken cancellationToken = default)
        {
            User user = await CreateBaseOrganisationUser(new OrganisationUserDTO(
                Email: organisationUserWithRoleDTO.Email,
                Password: organisationUserWithRoleDTO.Password,
                OrganisationName: organisationUserWithRoleDTO.OrganisationName), cancellationToken);

            Role? role = await _roleRepository.GetRoleByName(organisationUserWithRoleDTO.RoleName) ??
                throw new NotFoundException($"Role {organisationUserWithRoleDTO.RoleName} does not exist");

            user.Roles.Add(role);

            await SaveUser(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _authRepository.FindByEmailAsync(email);
        }

        private async Task<User> CreateBaseOrganisationUser(OrganisationUserDTO organisationUser, CancellationToken cancellationToken = default)
        {
            // validate user signup details 
            var createOrganisationUserValidator = new CreateOrganisationUserDTOValidator(_authRepository);
            var validationResult = await createOrganisationUserValidator.ValidateAsync(organisationUser, cancellationToken);

            if (validationResult.Errors.Any())
            {
                IDictionary<string, string> errors = validationResult
                    .Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                throw new ValidationException("Validation errors", errors);
            }
            // Get organisation id
            Organisation organisation = await _organisationRepository.GetOrganisationByName(organisationUser.OrganisationName) ??
                throw new NotFoundException($"Organisation with name {organisationUser.OrganisationName} not found");

            User user = CreateUserWithHashedPassword(organisationUser.Email, organisationUser.Password);
            user.OrganisationId = organisation.Id;

            return user;
        }

        private async Task SaveUser(User user)
        {
            try
            {
                await _authRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex);
            }
        }

        private User CreateUserWithHashedPassword(string email, string password)
        {
            string hashedPassword = EncryptionUtilities.HashString(password, _hashConfig.SecretKey);
            var user = new User()
            {
                Email = email,
                Password = hashedPassword
            };

            return user;
        }
    }
}

