using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class UnauthorizedException(string message) : BaseException(message)
	{
		public override IEnumerable<string> Errors { get; set; } = [];

		public override int StatusCode => (int)HttpStatusCode.Unauthorized;
	}
}
