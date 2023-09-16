using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Matario.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyFilter : Attribute, IAsyncActionFilter
	{
		public ApiKeyFilter()
		{
		}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // check if headers have X-Api-Key
            var ApiKey = "X-Api-Key";
            if (
                !context.HttpContext.Request.Headers.ContainsKey(ApiKey) ||
                context.HttpContext.Request.Headers[ApiKey] != "ubongKeyRequired")
            {
                throw new Exception("Invalid api key");
            }
            await next();
        }
    }
}

