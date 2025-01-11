namespace Cv.Guard.Api.Core.Exceptions
{
	public abstract class BaseException : Exception
	{
		protected abstract IEnumerable<string> Errors { get; set; }
		protected abstract int StatusCode { get; }

		public BaseException(string message)
			: base(message) { }

		public BaseException(string message, Exception innerException)
			: base(message, innerException) { }

		public BaseException(string message, IEnumerable<string> errors)
			: base(message)
		{
			Errors = errors;
		}
	}
}
