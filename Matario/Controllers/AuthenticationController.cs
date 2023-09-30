using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Application.Features.Queries.AuthenticationModule.Requests;
using Matario.Constants;
using Matario.Controllers.Common;
using Matario.Domain.Entities.AuthenticationModule;
using Matario.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static Matario.Application.Constants.ApplicationConstants;

namespace Matario.Controllers
{
    [EnableRateLimiting("fixed")]
    [ApiController]
	[Route($"{ApiConstant.RouteConstants.OrganisationBaseRoute}/[controller]")]
	public class AuthenticationController : BaseApiController
    {
		private readonly IMediator _mediator;
		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[EnableRateLimiting("fixed")]
		[HttpPost("[action]")]
		public async Task<AuthenticationResponse> Signin(SigninRequest signinRequest)
		{
			return await _mediator.Send(signinRequest);
		}

        [TypeFilter(typeof(BelongsToOrganisationFilter), Arguments = new object[] { PermissionConstants.CreateManager })]
        [HttpPost("[action]")]
        public async Task<User?> CreateOrganisationManager(AdminCreateUserWithRoleCommand command)
        {
            HttpContext.Items.TryGetValue(ApiConstant.RouteConstants.OrganisationNameKeyInRoute, out var organisationName);
			command.OrganisationName = organisationName as string ?? string.Empty;
            return await _mediator.Send(command);
        }
    }

}

