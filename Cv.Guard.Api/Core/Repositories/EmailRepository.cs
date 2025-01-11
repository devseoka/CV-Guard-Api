using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Core.Repositories
{
	public class EmailRepository(CvgaContext context) : IEmailRepository
	{
		public IQueryable<Email> Emails => context.Emails;

		public async Task<Email> Add(Email email)
		{
			var entry = context.Emails.Add(email);
			await context.SaveChangesAsync();
			return entry.Entity;
		}

		public async Task<IEnumerable<Email>> GetEmails()
		{
			return await Emails.ToListAsync();
		}

		public async Task<IEnumerable<Email>> GetEmailsByStatus(bool status)
		{
			return await context.Emails.Where(e => e.Status == status).ToListAsync();
		}
	}
}
