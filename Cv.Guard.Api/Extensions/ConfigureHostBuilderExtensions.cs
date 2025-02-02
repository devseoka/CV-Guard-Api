using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Data;

public static class ConfigureHostBuilderExtensions
{
	/// <summary>
	/// Configures Serilog for the WebApplicationBuilder with specified connection string.
	/// </summary>
	/// <param name="builder">The WebApplicationBuilder to configure.</param>
	/// <param name="connection">The connection string for the SQL Server database.</param>
	/// <returns>The configured WebApplicationBuilder.</returns>
	public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder, string connection)
	{
		var columnOptions = new ColumnOptions
		{
			Exception = { ColumnName = "ExceptionDetails" },
			Level = { ColumnName = "LogLevel", DataType = SqlDbType.NVarChar, DataLength = 50 },
			PrimaryKey = { ColumnName = "Id", DataType = SqlDbType.UniqueIdentifier },
			MessageTemplate = { ColumnName = "MessageTemplate", DataType = SqlDbType.NVarChar, DataLength = -1 },
			Message = { ColumnName = "Message", DataType = SqlDbType.NVarChar, DataLength = -1 }
		};

		var sinkOptions = new MSSqlServerSinkOptions
		{
			AutoCreateSqlTable = true,
			TableName = "Logs",
			SchemaName = "audit",
		};

		Serilog.Debugging.SelfLog.Enable(Console.Error);

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
