using System.Net;
using Cv.Guard.Api.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cv.Guard.Api.Helpers.Middleware;

public class ExceptionMiddleware() : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken
	)
	{
		if (exception is not NotFoundException)
		{
			string message = $"{exception.Message} - {exception.InnerException?.Message}";
			Log.Error(
				exception,
				"An unexpected error occurred while executing @{Path}. Error: @{Message}",
				httpContext.Request.Path,
				message
			);
		}
		await HandleExceptionAsync(httpContext, exception, cancellationToken);
		return true;
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception ex, CancellationToken cancellation)
	{
		context.Response.ContentType = "appplication/json";
		var error = new ProblemDetails { Detail = ex.Message, Instance = context.Request.Path };
		switch (ex)
		{
			case BaseException exception:
				context.Response.StatusCode = exception.StatusCode;
				error.Status = exception.StatusCode;
				if (exception.Errors is not null)
				{
					error.Extensions["errors"] = exception.Errors;
				}
				await context.Response.WriteAsJsonAsync(error, cancellation);
				break;
			default:
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				error.Status = context.Response.StatusCode;
				error.Extensions["errors"] = new[]
				{
					"Something went wrong on our end. Please try again later. If the issue persists, contact our support team at im@seokamoshele.digital",
				};
				await context.Response.WriteAsJsonAsync(error, cancellation);
				break;
		}
	}
}
