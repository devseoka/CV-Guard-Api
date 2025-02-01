using Azure.Storage.Blobs;
using Cv.Guard.Api.Configuration;
using Cv.Guard.Api.Contracts.Repositories;
using Cv.Guard.Api.Contracts.Services;
using Cv.Guard.Api.Contracts.Validators;
using Cv.Guard.Api.Core.Repositories;
using Cv.Guard.Api.Core.Validators;
using Cv.Guard.Api.Helpers.Filters;
using Cv.Guard.Api.Services;
using IpStack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
			services.AddSingleton((sp) =>
			{
				var azureBlobSettings = sp.GetRequiredService<IOptions<AzureBlobConfig>>().Value;
				var client = new BlobServiceClient(azureBlobSettings.ConnectionString);
				return client;
			});
			services.AddSingleton((sp) =>
			{
				var azureBlobSettings = sp.GetRequiredService<IOptions<AzureBlobConfig>>().Value;
				var client = new BlobServiceClient(azureBlobSettings.ConnectionString);
				var containerClient = client.GetBlobContainerClient(azureBlobSettings.Name);
				return containerClient;
			});
			return services;
		}

		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IUploadService, UploadService>();
			services.TryAddScoped<ILocationService, LocationService>();
			services.AddSingleton<ApiKeyAuthorizationFilter>();
			services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
			return services;
		}

		public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
		{
			services.AddScoped<IUploadRepository, UploadRepository>();
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<IEmailRepository, EmailRepository>();

			return services;
		}

		public static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connection)
		{
			services.AddDbContext<CvgaContext>(
				(options) =>
				{
					options.UseSqlServer(connection);
				}
			);
			return services;
		}
		/// <summary>
		/// Configures Swagger for the application by adding API key authentication.
		/// </summary>
		/// <param name="services">The IServiceCollection to add the Swagger configuration to.</param>
		/// <returns>The IServiceCollection with the Swagger configuration added.</returns>
		public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Name = "X-API-Key",
					Type = SecuritySchemeType.ApiKey,
					Description = "API Key Authentication"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "ApiKey"
							}
						},
						Array.Empty<string>()
					}
				});
			});
			return services;
		}
		public static IServiceCollection EnableCORS(this IServiceCollection services, string name)
		{
			services.AddCors((options) =>
			{
				options.AddPolicy(name, policy =>
				{
					policy
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});
			return services;
		}
	}
}
