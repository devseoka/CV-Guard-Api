using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Contracts.Repositories
{
	public interface IEmailRepository
	{
		IQueryable<Email> Emails { get; }
		Task<Email> Add(Email email);
		Task<IEnumerable<Email>> GetEmails();
		Task<IEnumerable<Email>> GetEmailsByStatus(bool status);
	}
}
