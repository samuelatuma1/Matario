using System;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Matario.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : BaseApiController
    {
		private readonly IMediator _mediator;
		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[EnableRateLimiting("fixed")]
		[HttpPost("[action]")]
		public async Task<string> Signup(SignupRequest signupRequest)
		{
			return await _mediator.Send(signupRequest);
		}
	}
}

