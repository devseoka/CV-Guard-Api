using System.Net;
using Cv.Guard.Api.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cv.Guard.Api.Helpers.Middleware;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IExceptionHandler
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
			logger.LogError(
				exception,
				"An unexpected error occurred while executing {Path}. Error: {ErrorMessage}",
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
				if (exception.Errors.Any())
				{
					error.Extensions["Errors"] = exception.Errors;
				}
				await context.Response.WriteAsJsonAsync(error, cancellation);
				break;
			default:
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				error.Status = context.Response.StatusCode;
				error.Extensions["Errors"] = new[]
				{
					"Something went wrong on our end. Please try again later. If the issue persists, contact our support team at support@hazie.co.za",
				};
				await context.Response.WriteAsJsonAsync(error, cancellation);
				break;
		}
	}
}
