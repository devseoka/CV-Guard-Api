using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cv.Guard.Api.Core.Configuration
{
	public class UploadConfiguration : IEntityTypeConfiguration<Upload>
	{
		public void Configure(EntityTypeBuilder<Upload> builder)
		{
			builder.HasIndex(u => u.Path).HasDatabaseName("IX_Uploads_Path").IsUnique();
			builder.Property(u => u.Path).IsRequired();

			builder.HasIndex(u => u.Key).HasDatabaseName("IX_Uploads_Key").IsUnique();
			builder.Property(u => u.Key).IsRequired();
		}
	}
}
