using System;
using AutoMapper;
using Matario.Application.Config;
using Matario.Application.Contracts.DataAccess.AuthenticationModule;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Contracts.UoW;
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
        private readonly IUserService _userService;
        private readonly IManageJwtService _manageJwtService;
        public SignupRequestHandler( IUserService userService, IManageJwtService manageJwtService)
        {
            _userService = userService;
            _manageJwtService = manageJwtService;
        }

        public async Task<AuthenticationResponse> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            SignUpDTO signUpDTO = new (Email: request.Email, Password: request.Password);

            // Creates a user. Ensures user signs up with a valid and unique email.
            await _userService.CreateUser(signUpDTO, cancellationToken);

            // Creates access and Refresh token for the created user
            var user = await _userService.GetUserByEmail(request.Email);
            AuthenticationResponse authenticationResponse = await _manageJwtService.GenerateAccessAndRefreshToken(user);

            return authenticationResponse;
        }
    }
}

