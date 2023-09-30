using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.DTOs.AuthenticationModule;
using Matario.Application.Exceptions;
using Matario.Constants;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matario.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BelongsToOrganisationFilter : Attribute, IAsyncActionFilter
	{
        public string Permissions { get; set; } = string.Empty;
		public BelongsToOrganisationFilter()
		{
		}

        public BelongsToOrganisationFilter(string permissions)
        {
            Permissions = permissions;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty) ?? string.Empty;


            string organisationNameKeyInRoute = ApiConstant.RouteConstants.OrganisationNameKeyInRoute;
            string organisationName =  context.HttpContext.GetRouteValue(organisationNameKeyInRoute)?.ToString() ?? string.Empty;


            var manageJwtService =  context.HttpContext.RequestServices.GetRequiredService<IManageJwtService>();

            HasPermissionAndOrganisationNameDTO permissionAndOrganisationName = await manageJwtService.ValidatePermissionAndGetOrganisationName(token, Permissions);

            ValidatePermissionAndOrganisationName(permissionAndOrganisationName, organisationName);

            // add organisationName to request so endpoints can have access to the organisation
            AddOrganisationNameToRequest(context, permissionAndOrganisationName.OrganisationName);

            await next();
        }

        public void ValidatePermissionAndOrganisationName(HasPermissionAndOrganisationNameDTO permissionAndOrganisationName, string organisationName)
        {
            if (string.IsNullOrEmpty(organisationName))
            {
                throw new UnAuthorizedException("Invalid organisation");
            }
            if (!permissionAndOrganisationName.HasPermission)
            {
                throw new UnAuthorizedException("Not allowed");
            }

            if (!permissionAndOrganisationName.OrganisationName.Equals(organisationName, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnAuthorizedException($"Not allowed to carry out this action in organisation {organisationName}");
            }
        }

        public void AddOrganisationNameToRequest(ActionExecutingContext context, string organisationName)
        {
            context.HttpContext.Items[ApiConstant.RouteConstants.OrganisationNameKeyInRoute] = organisationName;
        }
    }
}

