using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace Cv.Guard.Api.Extensions
{
	public static class ConfigureHostBuilderExtensions
	{
		public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder, string connection)
		{
			var columnOptions = new ColumnOptions
			{
				Exception = { ColumnName = "ExceptionDetails" },
				Level = { ColumnName = "LogLevel", DataType = SqlDbType.NVarChar, DataLength = 50 },
				MessageTemplate = { ColumnName = "MessageTemplate", DataType = SqlDbType.NVarChar, DataLength = -1 },
				Message = { ColumnName = "Message", DataType = SqlDbType.NVarChar, DataLength = -1 }

			};

			var sinkOptions = new MSSqlServerSinkOptions
			{
				AutoCreateSqlTable = true,
				TableName = "Logs",
				SchemaName = "audit",
			};

			// Enable Serilog self-logging (useful for debugging)
			Serilog.Debugging.SelfLog.Enable(Console.Error);

			// Configure Serilog
			builder.Host.UseSerilog((context, services, loggerConfiguration) =>
				loggerConfiguration
					.Enrich.FromLogContext()
					.Enrich.WithProperty("MachineId", Environment.MachineName)
					.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
					.WriteTo.MSSqlServer(
						connectionString: connection,
						sinkOptions: sinkOptions,
						columnOptions: columnOptions,
						restrictedToMinimumLevel: LogEventLevel.Error
					)
			);

			return builder;
		}

	}
}
