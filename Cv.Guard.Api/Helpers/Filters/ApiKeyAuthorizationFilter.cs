using Cv.Guard.Api.Contracts.Validators;
using Cv.Guard.Api.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cv.Guard.Api.Helpers.Filters
{
	public class ApiKeyAuthorizationFilter(IApiKeyValidator validator) : IAuthorizationFilter
	{
		private const string HEADER = "X-API-Key";

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string apiKey = context.HttpContext.Request.Headers[HEADER];
			
			if (string.IsNullOrEmpty(apiKey))
			{
				string message = "API key is required.";
				throw new UnauthorizedException(message, [message]);
			}

			if (!validator.IsValid(apiKey))
			{
				var message = "Your API key is invalid.";
				throw new UnauthorizedException(message, [message]);
			}
		}
	}
}
