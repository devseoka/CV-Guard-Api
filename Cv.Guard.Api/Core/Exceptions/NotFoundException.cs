using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
    public class NotFoundException(string message) : BaseException(message)
    {
        protected override IEnumerable<string> Errors { get; set; }

        protected override int StatusCode => (int)HttpStatusCode.NotFound;
    }
}
