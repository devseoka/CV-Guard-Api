using System.Reflection;
using Cv.Guard.Api.Extensions;
using Cv.Guard.Api.Helpers.Middleware;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureExternalServices();

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cv Guard API", Version = "v1" });
	c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
});

builder.Host.UseSerilog();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
