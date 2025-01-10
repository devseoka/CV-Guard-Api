using System.Net;

namespace Cv.Guard.Api.Core.Exceptions
{
    public class ConflictException(string message, IEnumerable<string> errors)
        : BaseException(message, errors)
    {
        protected override IEnumerable<string> Errors { get; set; } = errors;

        protected override int StatusCode => (int)HttpStatusCode.Conflict;
    }
}
