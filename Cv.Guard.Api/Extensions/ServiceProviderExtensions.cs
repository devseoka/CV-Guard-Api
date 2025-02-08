using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static IServiceProvider EnsureDatabaseCreated(this IServiceProvider services)
		{
            
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CvgaContext>();
			context.Database.ExecuteSqlRaw(@"IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'audit')
			BEGIN EXEC('CREATE SCHEMA audit'); END");
			context.Database.Migrate();
            return services;
        }
	}
}
