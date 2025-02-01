using System.Text.RegularExpressions;
using Cv.Guard.Api.Contracts.Validators;

namespace Cv.Guard.Api.Core.Validators;

public class ApiKeyValidator : IApiKeyValidator
{
	private const string PATTERN =@"^[A-Z]{2,3}-\d{10}-[A-Za-z0-9]+-[A-Za-z0-9]+$";

	public bool IsValid(string apiKey)
	{
		if (string.IsNullOrEmpty(apiKey))
		{
			return false;
		}

		return Regex.IsMatch(apiKey, PATTERN);
	}
}
