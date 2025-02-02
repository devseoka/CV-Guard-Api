using Cv.Guard.Api.Core.Dto;
using FluentValidation;

namespace Cv.Guard.Api.Core.Validators
{
	public class DownloadValidator : AbstractValidator<EmailRequest>
	{
		private readonly List<string> _freeEmailDomains =
		[
			"@gmail.com",
			"@yahoo.com",
			"@ymail.com",
			"@rocketmail.com",
			"@outlook.com",
			"@hotmail.com",
			"@live.com",
			"@icloud.com",
			"@me.com",
			"@mac.com",
			"@aol.com",
			"@proton.me",
			"@protonmail.com",
			"@zoho.com",
			"@gmx.com",
			"@gmx.de",
			"@mail.com",
			"@yandex.com",
			"@yandex.ru",
		];

		public DownloadValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email is required.")
				.EmailAddress()
				.WithMessage("Invalid email format.")
				.Must(email => !IsFreeEmailProvider(email))
				.WithMessage(
					"Email from free email providers is not allowed. Use custom domains, i.e user@abc-company.com"
				);
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Full Name is required.")
				.Matches(@"^[a-zA-Z\s]+$")
				.WithMessage("Full Name should only contain letters and spaces.")
				.Length(2, 100)
				.WithMessage("Full Name must be between 2 and 100 characters.");
		}

		private bool IsFreeEmailProvider(string email)
		{
			var domain = email?.Split('@').LastOrDefault()?.ToLowerInvariant();
			return _freeEmailDomains.Any(freeDomain => domain?.EndsWith(freeDomain) == true);
		}
	}
}
