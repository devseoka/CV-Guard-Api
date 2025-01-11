using Cv.Guard.Api.Contracts.Validators;
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
				throw new UnauthorizedAccessException("API key is required.");
			}

			if (!validator.IsValid(apiKey))
			{
				throw new UnauthorizedAccessException("Your API key is invalid.");
			}
		}
	}
}
