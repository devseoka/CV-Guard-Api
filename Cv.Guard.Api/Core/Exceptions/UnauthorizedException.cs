using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class UnauthorizedException : BaseException
	{
		public UnauthorizedException(string message) : base(message)
		{
		}

		public UnauthorizedException(string message, IEnumerable<string> errors) : base(message, errors)
		{
		}

		public override int StatusCode => (int)HttpStatusCode.Unauthorized;
	}

}
