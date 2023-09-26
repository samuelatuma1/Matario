using System;
using System.Net;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Matario.Application.Exceptions;
using Matario.Domain.Enums.AuthenticationModule;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matario.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IsSuperAdminFilter<T> : Attribute, IAsyncActionFilter
        where T : IManageJwtService
    {
        private readonly IManageJwtService _manageJwtService;
        public IsSuperAdminFilter(IManageJwtService manageJwtService)
        {
            _manageJwtService = manageJwtService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            bool userIsSuperAdmin = await _manageJwtService.IsSuperAdmin(token);
            
            if(!userIsSuperAdmin)
            {
                throw new UnAuthorizedException("User not super admin");
            }
            await next();
        }
    }
}

