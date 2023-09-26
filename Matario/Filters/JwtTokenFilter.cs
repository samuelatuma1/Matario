using System;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matario.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtTokenFilter<T> : Attribute, IAsyncActionFilter
        where T : IManageJwtService
	{
        private readonly IManageJwtService _manageJwtService;
		public JwtTokenFilter(IManageJwtService manageJwtService)
		{
            _manageJwtService = manageJwtService;
		}

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            throw new NotImplementedException();
        }
    }
}

