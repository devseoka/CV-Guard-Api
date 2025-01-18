using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
	public class NotFoundException(string message) : BaseException(message)
	{
		public override int StatusCode => (int)HttpStatusCode.NotFound;
	}
}
