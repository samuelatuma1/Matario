using System;
using System.Net;
using Matario.Application.Exceptions;
using Matario.Models;

namespace Matario.Middlewares
{
	public class ExceptionMiddleware 
	{
		private readonly RequestDelegate _next;
		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch(Exception ex)
			{
				await HandleException(ex, httpContext);
			}
		}

		public async Task HandleException(Exception ex, HttpContext httpContext)
		{
			var statusCode = HttpStatusCode.InternalServerError;
			var errorResponse = new ErrorModel();

			switch (ex)
			{
				case ValidationException exception:
					statusCode = HttpStatusCode.BadRequest;
					errorResponse.Message = exception.Message;
					errorResponse.Errors = exception.Errors;
					break;
				case UnAuthorizedException exception:
					statusCode = HttpStatusCode.Unauthorized;
					errorResponse.Message = exception.Message;
					break;
                default:
					errorResponse.Message = ex.Message;
					break;
			}
			httpContext.Response.StatusCode = (int)statusCode;
			await httpContext.Response.WriteAsJsonAsync(errorResponse);
		}
	}
}

