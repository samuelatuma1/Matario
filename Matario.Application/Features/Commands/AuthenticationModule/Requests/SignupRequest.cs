using System;
using MediatR;

namespace Matario.Application.Features.Commands.AuthenticationModule.Requests
{
	public record SignupRequest(string Email, string Password) : IRequest<string>
	{
    }
}

