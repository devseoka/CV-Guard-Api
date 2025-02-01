using Microsoft.EntityFrameworkCore;

namespace Cv.Guard.Api.Extensions
{
	public static class ServiceProviderExtensions
	{
        public static IServiceProvider EnsureDataCreated(this IServiceProvider services){
            
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CvgaContext>();
            
            context.Database.EnsureCreated();
            context.Database.Migrate();
            
            return services;
        }
	}
}