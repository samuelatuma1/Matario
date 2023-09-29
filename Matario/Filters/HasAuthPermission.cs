using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matario.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasAuthPermission : Attribute, IAsyncActionFilter
	{

        public string RequiredPermissions { get; set; } = string.Empty;

        public HasAuthPermission()
        {
        }

        public HasAuthPermission(string requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var _manageJwtService = context.HttpContext.RequestServices.GetRequiredService<IManageJwtService>();
            bool userHasPermission = await _manageJwtService.UserHasPermission(token, RequiredPermissions);
            if (!userHasPermission)
            {
                throw new UnAuthorizedException("Not allowed");
            }

            //TODO: Add organisation Name to User request somehow
            await next();
        }
    }
}

