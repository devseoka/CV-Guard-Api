namespace Cv.Guard.Api.Contracts.Validators
{
	public interface IApiKeyValidator
	{
		bool IsValid(string apiKey);
	}
}
