using System;
using AutoMapper;
using Matario.Application.Config;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Exceptions;
using Matario.Application.Features.Commands.AuthenticationModule.Common;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Application.Features.Commands.AuthenticationModule.Validators;
using Matario.Application.Utilities;
using Matario.Domain.Entities.AuthenticationModule;
using MediatR;
using Microsoft.Extensions.Options;

namespace Matario.Application.Features.Commands.AuthenticationModule.Handlers
{
	public class SignupRequestHandler : IRequestHandler<SignupRequest, string>
	{
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly HashConfig _hashConfig;
        public SignupRequestHandler(IMapper mapper, IAuthenticationRepository authRepository, IJwtService jwtService, IOptions<HashConfig> hashConfig)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _jwtService = jwtService;
            _hashConfig = hashConfig.Value;
        }

        public async Task<string> Handle(SignupRequest request, CancellationToken cancellationToken)
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
            await _authRepository.AddAsync(user);
            
            // convert some data to jwt token
            return ManageJwt.GenerateToken(_jwtService, user);
        }
    }
}

