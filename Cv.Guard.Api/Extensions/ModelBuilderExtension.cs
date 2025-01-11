using Cv.Guard.Api.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Extensions
{
	public static class ModelBuilderExtension
	{
		public static ModelBuilder ConfigureRelations(this ModelBuilder builder){
			builder.ApplyConfiguration(new EmailConfiguration());
			builder.ApplyConfiguration(new LocationConfiguration());
			builder.ApplyConfiguration(new UploadConfiguration());
			return builder;
		}
	}
}
