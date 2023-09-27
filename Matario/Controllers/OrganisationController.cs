using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Features.Commands.OrganisationModule.Commands;
using Matario.Controllers.Common;
using Matario.Domain.Entities.OrganisationModule;
using Matario.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Matario.Controllers
{
    [EnableRateLimiting("fixed")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrganisationController : BaseApiController
    {
        private readonly IMediator _mediator;
        public OrganisationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpPost("[action]")]
        public async Task<Organisation> CreateOrganisation(CreateOrganisationCommand createOrganisationCommand)
        {
            return await _mediator.Send(createOrganisationCommand);
        }
    }
}

