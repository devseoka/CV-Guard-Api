using Cv.Guard.Api.Configuration;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

namespace Cv.Guard.Api.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection ConfigureOptions(
			this IServiceCollection services,
			IConfiguration configuration
		)
		{
			services.Configure<AzureBlobConfig>(configuration.GetSection("AzureBlob"));
			services.Configure<PostmarkConfig>(configuration.GetSection("Postmark"));
			return services;
		}

		public static IServiceCollection ConfigurePostmark(this IServiceCollection services)
		{
			services.AddSingleton(
				(sp) =>
				{
					var postmarkSettings = sp.GetRequiredService<IOptions<PostmarkConfig>>().Value;
					var client = new PostmarkClient(postmarkSettings.Key);
					return client;
				}
			);
			return services;
		}
	}
}
