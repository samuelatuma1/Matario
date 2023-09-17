using System;
using MediatR;

namespace Matario.Application.Features.Queries.AuthenticationModule.Requests
{
	public record SigninRequest(string Email, string Password) : IRequest<string>
	{
		
	}
}

