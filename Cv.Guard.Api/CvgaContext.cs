using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api
{
	public class CvgaContext(DbContextOptions<CvgaContext> options) : DbContext(options)
	{
		public DbSet<Location> Locations { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Upload> Uploads { get; set; }
	}
}
