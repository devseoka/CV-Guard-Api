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
            : base(message, errors) => Errors = errors;

        protected override IEnumerable<string> Errors { get; set; }

        protected override int StatusCode => (int)HttpStatusCode.InternalServerError;
    }
}
