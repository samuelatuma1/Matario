using System;
using Matario.Application.Features.Queries.AuthenticationModule.Requests;
using Matario.Application.Exceptions;
using Matario.Application.Features.Queries.AuthenticationModule.Validators;
using MediatR;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Utilities;
using Matario.Application.Config;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Domain.Entities.AuthenticationModule;

namespace Matario.Application.Features.Queries.AuthenticationModule.Handlers
{
	public class SigninRequestHandler : IRequestHandler<SigninRequest, AuthenticationResponse>
	{
        private readonly IAuthenticationRepository _authRepository;
        private readonly HashConfig _hashConfig;
        private readonly IManageJwtService _manageJwtService;
        public SigninRequestHandler(IAuthenticationRepository authRepository, IOptions<HashConfig> hashConfig, IManageJwtService manageJwtService)
        {
            _authRepository = authRepository;
            _hashConfig = hashConfig.Value;
            _manageJwtService = manageJwtService;
        }

        public async Task<AuthenticationResponse> Handle(SigninRequest request, CancellationToken cancellationToken)
        {
			// Validate signin request
			var signinRequestValidator = new SigninRequestValidator();
            var validatedRequest = await signinRequestValidator.ValidateAsync(request, cancellationToken);
            // if valid
            if (validatedRequest.Errors.Any())
            {
                IDictionary<string, string> errors = validatedRequest.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                throw new ValidationException("Validation error occured", errors);
            }
            // hash password
            User user = await _authRepository.FindByEmailAsync(request.Email) ?? throw new AuthenticationException(string.Format("user with email {0} not found", request.Email));
            ValidateUserPasswordOrRaiseException(user, request.Password);

            return await _manageJwtService.GenerateAccessAndRefreshToken(user);
        }

        private void ValidateUserPasswordOrRaiseException(User user, string password)
        {
            var hashedRequestPassword = EncryptionUtilities.HashString(password, _hashConfig.SecretKey);
            if (!user.Password.Equals(hashedRequestPassword))
            {
                throw new AuthenticationException(string.Format("password invalid for email {0}", user.Email));
            }
        }
	}
}

