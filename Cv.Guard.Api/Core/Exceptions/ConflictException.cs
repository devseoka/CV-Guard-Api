using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class ConflictException(string message, IEnumerable<string> errors) : BaseException(message, errors)
	{
		public override IEnumerable<string> Errors { get; set; } = errors;

		public override int StatusCode => (int)HttpStatusCode.Conflict;
	}
}
