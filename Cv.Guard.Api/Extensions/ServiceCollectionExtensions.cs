using Azure.Storage.Blobs;
using Cv.Guard.Api.Configuration;
using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Contracts.Services;
using Cv.Guard.Api.Services;
using IpStack.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

namespace Cv.Guard.Api.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureOptions(
			this IServiceCollection services,
			IConfiguration configuration
		)
		{
			services.Configure<IpStackOptions>(configuration.GetSection("IpStack"));
			services.Configure<AzureBlobConfig>(configuration.GetSection("AzureBlob"));
			services.Configure<PostmarkConfig>(configuration.GetSection("Postmark"));
			return services;
		}

		public static IServiceCollection ConfigureExternalServices(this IServiceCollection services)
		{
			services.AddSingleton(
				(sp) =>
				{
					var postmarkSettings = sp.GetRequiredService<IOptions<PostmarkConfig>>().Value;
					var client = new PostmarkClient(postmarkSettings.Key);
					return client;
				}
			);
			services.AddSingleton(
				(sp) =>
				{
					var azureBlobSettings = sp.GetRequiredService<IOptions<AzureBlobConfig>>().Value;
					var client = new BlobServiceClient(azureBlobSettings.ConnectionString);
					return client;
				}
			);
			return services;
		}

		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IUploadService, UploadService>();
			return services;
		}

		public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
		{
			services.TryAddScoped<IUploadRepository, UploadRepository>();
			return services;
		}

		public static IServiceCollection ConfigureValidators(this IServiceCollection services)
		{
			services.TryAddScoped<IUploadRepository, UploadRepository>();
			return services;
		}
	}
}
