using Cv.Guard.Api.Core.Dto;
using Cv.Guard.Api.Core.Models;
using FluentValidation;

namespace Cv.Guard.Api.Core.Validators
{
	public class UploadValidator : AbstractValidator<UploadRequest>
	{
		public UploadValidator()
		{
			RuleFor(x => x.Initials)
				.NotEmpty()
				.WithMessage("Initials are required.")
				.Length(2, 3)
				.WithMessage("Initials must be between 2 and 5 characters.");

			RuleFor(x => x.File)
				.NotNull()
				.WithMessage("File is required.")
				.Must(f => f.FileName.EndsWith(".docx") || f.FileName.EndsWith(".pdf"))
				.WithMessage("Your cv should be .docx or .pdf file types.");
		}
	}
}
