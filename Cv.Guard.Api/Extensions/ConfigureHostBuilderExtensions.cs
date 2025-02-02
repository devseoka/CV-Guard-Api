using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using static Serilog.Sinks.MSSqlServer.ColumnOptions;

namespace Cv.Guard.Api.Extensions
{
	public static class ConfigureHostBuilderExtensions
	{
		/// <summary>
		/// Configures Serilog for the specified <see cref="ConfigureHostBuilder"/> instance.
		/// </summary>
		/// <param name="host">The <see cref="ConfigureHostBuilder"/> instance to configure.</param>
		/// <param name="connection">The connection string for the PostgreSQL database.</param>
		/// <returns>The configured <see cref="ConfigureHostBuilder"/> instance.</returns>
		/// <remarks>
		/// This method sets up Serilog to log to the console with a minimum level of Debug and to a PostgreSQL database
		/// with a minimum level of Error. The PostgreSQL table will be created automatically if it does not exist.
		/// </remarks>
		public static ConfigureHostBuilder ConfigureSerilog(this ConfigureHostBuilder host, string connection)
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
		
		   host.UseSerilog((context, services, loggerConfiguration) =>
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
		   )
			return host;
		}

	}
}
