using PostmarkDotNet;

namespace Cv.Guard.Api.Contracts.Services
{
	/// <summary>
	/// Interface for email service to send emails asynchronously.
	/// </summary>
	/// <param name="email">The email message to be sent.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the response from Postmark.</returns>
	public interface IEmailService
	{
		/// <summary>
		/// Sends an email asynchronously.
		/// </summary>
		/// <param name="email">The email message to be sent.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the response from Postmark.</returns>
		Task<PostmarkResponse> SendAsync(PostmarkMessage email);

		/// <summary>
		/// Sends a templated email asynchronously.
		/// </summary>
		/// <param name="email">The templated email message to be sent.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the response from Postmark.</returns>
		Task<PostmarkResponse> SendAsync(TemplatedPostmarkMessage email);
	}
}
