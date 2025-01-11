using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Core.Repositories
{
	public class UploadRepository(CvgaContext context) : IUploadRepository
	{
		public IQueryable<Upload> Uploads => context.Uploads;

		public async Task<Upload> Add(Upload upload)
		{
			var entry = context.Uploads.Add(upload);
			await context.SaveChangesAsync();
			return entry.Entity;
		}

		public async Task<Upload> GetUploadByKey(string key)
		{
			return await context.Uploads.FirstOrDefaultAsync(u => u.Key == key);
		}

		public async Task<IEnumerable<Upload>> GetUploads()
		{
			return await context.Uploads.ToListAsync();
		}
	}
}
