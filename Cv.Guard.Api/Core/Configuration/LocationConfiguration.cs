using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cv.Guard.Api.Core.Configuration
{
	public class LocationConfiguration : IEntityTypeConfiguration<Location>
	{
		public void Configure(EntityTypeBuilder<Location> builder)
		{
			builder.HasIndex(l => l.City).HasDatabaseName("IX_Locations_City").IsUnique();
			builder.HasIndex(l => l.Region).HasDatabaseName("IX_Locations_Region").IsUnique();
		}
	}
}
