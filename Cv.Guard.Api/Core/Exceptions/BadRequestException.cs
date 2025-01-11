using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class BadRequestException : BaseException
	{
		public BadRequestException(string message)
			: base(message) { }

		public BadRequestException(string message, Exception innerException)
			: base(message, innerException) { }

		public BadRequestException(string message, IEnumerable<string> errors)
			: base(message, errors) => Errors = errors;

		protected override IEnumerable<string> Errors { get; set; }
		protected override int StatusCode => (int)HttpStatusCode.BadRequest;
	}
}
