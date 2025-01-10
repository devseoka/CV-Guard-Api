using Cv.Guard.Api.Configuration;
using Cv.Guard.Api.Contracts.Services;
using Cv.Guard.Api.Core.Models;
using Microsoft.Extensions.Options;
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
	}
}
