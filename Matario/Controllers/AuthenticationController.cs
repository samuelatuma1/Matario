using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Features.Commands.AuthenticationModule.Requests;
using Matario.Application.Features.Queries.AuthenticationModule.Requests;
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
	[Route("api/v1/[controller]")]
	public class AuthenticationController : BaseApiController
    {
		private readonly IMediator _mediator;
		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<AuthenticationResponse> Signup(SignupRequest signupRequest)
		{
			return await _mediator.Send(signupRequest);
		}

		[EnableRateLimiting("fixed")]
		[HttpPost("[action]")]
		public async Task<AuthenticationResponse> Signin(SigninRequest signinRequest)
		{
			return await _mediator.Send(signinRequest);
		}

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpPost("[action]")]
		public async Task<Role> AddRole(CreateRoleRequest createRoleRequest)
		{
			return await _mediator.Send(createRoleRequest);
		}

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpGet("[action]")]
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
			var request = new GetAllRolesRequest();
            return await _mediator.Send(request);
        }


		[ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
		[HttpPatch("[action]")]
        public async Task<Role?> UpdateRole(UpdateRoleRequest updateRoleRequest)
		{
			return await _mediator.Send(updateRoleRequest);
		}

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpDelete("[action]/{id}")]
        public async Task DeleteRole([FromRoute]long id)
        {
			var deleteRoleRequest = new DeleteRoleRequest(id);
            await _mediator.Send(deleteRoleRequest);
        } 

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpPost("[action]")]
        public async Task<Permission> AddPermission(CreatePermissionRequest createPermissionRequest)
        {
            return await _mediator.Send(createPermissionRequest);
        }

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpPost("[action]")]
        public async Task<Role> AddRolePermission(AddRolePermissionRequest addRolePermissionRequest)
        {
            return await _mediator.Send(addRolePermissionRequest);
        }

        [ServiceFilter(typeof(IsSuperAdminFilter<IManageJwtService>))]
        [HttpPost("[action]")]
        public async Task<User?> CreateOrganisationCorporateAdmin(CreateOrganisationUserCommand command)
        {
            string corporateAdmin = RoleConstants.CorporateAdmin;
            CreateOrganisationUserWithRoleCommand createOrganisationUserWithRole = new (
                Email: command.Email,
                Password : command.Password,
                OrganisationName: command.OrganisationName,
                RoleName: corporateAdmin
                );
            return await _mediator.Send(createOrganisationUserWithRole);
        }

        [TypeFilter(typeof(HasAuthPermission), Arguments = new object[] { PermissionConstants.CreateManager })]
        [HttpPost("[action]")]
        public async Task<User?> CreateOrganisationManager(AdminCreateUserWithRoleCommand command)
        {
            return await _mediator.Send(command);
        }
    }

}

