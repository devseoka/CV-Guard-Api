using Cv.Guard.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cv.Guard.Api.Core.Configuration
{
	public class EmailConfiguration : IEntityTypeConfiguration<Email>
	{
		public void Configure(EntityTypeBuilder<Email> builder)
		{
			builder.HasIndex(e => e.Status).HasDatabaseName("IX_Emails_Status");
		}
	}
}
