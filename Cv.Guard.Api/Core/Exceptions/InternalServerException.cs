using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class InternalServerException : BaseException
	{
		public InternalServerException(string message)
			: base(message) { }

		public InternalServerException(string message, Exception innerException)
			: base(message, innerException) { }

		public InternalServerException(string message, IEnumerable<string> errors)
			: base(message, errors) {}

		public override int StatusCode => (int)HttpStatusCode.InternalServerError;
	}
}
