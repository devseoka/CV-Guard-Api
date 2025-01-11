using Cv.Guard.Api.Contracts.Services;
using PostmarkDotNet;

namespace Cv.Guard.Api.Services
{
	public class EmailService(PostmarkClient client) : IEmailService
	{
		public async Task<PostmarkResponse> SendAsync(PostmarkMessage email)
		{
			var response = await client.SendMessageAsync(email);
			return response;
		}

		public async Task<PostmarkResponse> SendAsync(TemplatedPostmarkMessage email)
		{
			var response = await client.SendMessageAsync(email);
			return response;
		}
	}
}
