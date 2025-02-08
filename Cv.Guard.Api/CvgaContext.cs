using Cv.Guard.Api.Contracts.Models;
using Cv.Guard.Api.Core.Models;
using Cv.Guard.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api
{
	public class CvgaContext(DbContextOptions<CvgaContext> options) : DbContext(options)
	{
		public DbSet<Location> Locations { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Upload> Uploads { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ConfigureRelations();
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var changedEntity in ChangeTracker.Entries())
			{
				if (changedEntity.Entity is IAuditTrail entity)
				{
					switch (changedEntity.State)
					{
						case EntityState.Added:
							entity.CreatedDate = DateTime.Now;
							entity.LastUpdated = DateTime.Now;
							entity.IsDeleted = false;
							break;
						case EntityState.Modified:
							Entry(entity).Property(x => x.CreatedDate).IsModified = false;
							Entry(entity).Property(x => x.DeletedDate).IsModified = false;
							entity.LastUpdated = DateTime.UtcNow;
							break;
					}
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
