using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static IServiceProvider EnsureDatabaseCreated(this IServiceProvider services)
		{
            
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CvgaContext>();
			context.Database.ExecuteSqlRaw("CREATE SCHEMA IF NOT EXISTS audit");
			context.Database.Migrate();
            return services;
        }
	}
}
