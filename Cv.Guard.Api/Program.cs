using System.Reflection;
using System.Text;
using Cv.Guard.Api.Extensions;
using Cv.Guard.Api.Helpers.Middleware;
using FluentValidation;
using IpStack.Extensions;
using IpStack.Models;
using Serilog;


try
{
	var builder = WebApplication.CreateBuilder(args);
	const string CORS_ORIGINS = "CV.Guard.Api-CORS-Origins";

	builder.Services.ConfigureOptions(builder.Configuration);
	builder.Services.ConfigureExternalServices();

	string connection = builder.Configuration.GetConnectionString("DefaultConnection");

	var ipStackOpts = builder.Configuration.GetSection("IpStack").Get<IpStackOptions>();
	builder.Services.AddIpStack(ipStackOpts.ApiKey);
	builder.Services.EnableCORS(CORS_ORIGINS);

	builder.Host.ConfigureSerilog(connection);
	builder.Services.ConfigureDbContext(connection);
	builder.Services.ConfigureRepositories();
	builder.Services.ConfigureServices();

	builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
	builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

	builder.Services.AddControllers();
	builder.Services.AddExceptionHandler<ExceptionMiddleware>();
	builder.Services.AddProblemDetails();

	builder.Services.ConfigureSwagger();
	builder.Host.UseSerilog();

	var app = builder.Build();

	app.UseCors(CORS_ORIGINS);
	app.UseExceptionHandler();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();
	app.UseRouting();
	app.Services.EnsureDatabaseCreated();

	app.MapControllers();

	app.Run();

}
catch (Exception ex) when (ex is not HostAbortedException)
{
	StringBuilder sb = new();
	sb.Append(ex.Message);
	if (ex.InnerException is not null)
	{
		sb.AppendLine($"Detailed Error Message: {ex.InnerException.Message}");
		sb.Append($" - {ex.InnerException?.InnerException?.Message}");
	}
	Log.Fatal(ex, "Host terminated unexpectedly. Error: @{Message}", sb.ToString());

}
