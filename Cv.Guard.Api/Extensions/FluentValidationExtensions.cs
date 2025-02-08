using Cv.Guard.Api.Core.Exceptions;
using FluentValidation;

namespace Cv.Guard.Api.Extensions
{
	public static class FluentValidationExtensions
	{
		public static void ValidateAndThrowConflictException<T>(this IValidator<T> validator, T instance)
		{
			var res = validator.Validate(instance);

			if (!res.IsValid)
			{
				throw new ConflictException("Validation failed", res.Errors.Select(e => e.ErrorMessage));
			}
		}
	}
}
