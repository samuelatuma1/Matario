using System;
using AutoMapper;
using Matario.Application.Config;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Application.Features.Commands.AuthenticationModule.Validators;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;
using Microsoft.Extensions.Options;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class SignupRequestHandler : IRequestHandler<SignupRequest, AuthenticationResponse>
	{
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authRepository;
        private readonly HashConfig _hashConfig;
        private readonly IManageJwtService _manageJwtService;
        public SignupRequestHandler(IMapper mapper, IAuthenticationRepository authRepository, IOptions<HashConfig> hashConfig, IManageJwtService manageJwtService)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _hashConfig = hashConfig.Value;
            _manageJwtService = manageJwtService;
        }

        public async Task<AuthenticationResponse> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            // validate sign up request
            var signupRequestValidator = new SignupRequestValidator(_authRepository);
            var signUpValidationResult = await signupRequestValidator.ValidateAsync(request, cancellationToken);

            if (signUpValidationResult.Errors.Any())
            {
                IDictionary<string, string> errors = signUpValidationResult.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                throw new ValidationException("Validation errors", errors);
            }

            // convert signup request to entity
            var user = _mapper.Map<User>(request);
            user.Password = EncryptionUtilities.HashString(user.Password, _hashConfig.SecretKey);
            // save signup request in database
            var savedUser = await _authRepository.AddAsync(user);

            // convert some data to jwt token
            AuthenticationResponse authenticationResponse = await _manageJwtService.GenerateAccessAndRefreshToken(user);

            return authenticationResponse;
        }
    }
}

