using System.Reflection;
using Cv.Guard.Api.Extensions;
using Cv.Guard.Api.Helpers.Middleware;
using FluentValidation;
using IpStack.Extensions;
using IpStack.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
const string CORS_ORIGINS = "CV.Guard.Api-CORS-Origins";

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureExternalServices();

string connection =  builder.Configuration.GetConnectionString("DefaultConnection");

var ipStackOpts = builder.Configuration.GetSection("IpStack").Get<IpStackOptions>();
builder.Services.AddIpStack(ipStackOpts.ApiKey);

builder.Services.ConfigureDbContext(connection);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionMiddleware>();
builder.Services.AddProblemDetails();
builder.Services.EnableCORS(CORS_ORIGINS);

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

app.MapControllers();

app.Run();
