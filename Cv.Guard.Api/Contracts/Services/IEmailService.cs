using PostmarkDotNet;

namespace Cv.Guard.Api.Contracts.Services
{
	public interface IEmailService
	{
		Task<PostmarkResponse> SendAsync(PostmarkMessage email);
	}
}
