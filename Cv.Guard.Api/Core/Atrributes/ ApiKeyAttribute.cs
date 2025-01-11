using Cv.Guard.Api.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cv.Guard.Api.Core.Atrributes
{
	public class ApiKeyAttribute : ServiceFilterAttribute
	{
		public ApiKeyAttribute()
			: base(typeof(ApiKeyAuthorizationFilter)) { }
	}
}
