using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;

namespace Cv.Guard.Api.Core.Repositories
{
	public class EmailRepository : IEmailRepository
	{
		public IQueryable<Email> Emails => throw new NotImplementedException();

		public Task<Email> Add(Email email)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Email>> GetEmails()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Email>> GetEmailsByStatus(bool status)
		{
			throw new NotImplementedException();
		}
	}
}
