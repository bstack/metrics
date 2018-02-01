using System;
using System.Threading;
using System.Threading.Tasks;
#if NETCORE
using System.IO;
using Microsoft.Extensions.Configuration;
#endif
#if NETFULL
using System.Configuration;
#endif


namespace Metric.Inv.v3.SampleUsage
{
	public class Program
	{
		public static readonly ConfigurationSettings ConfigurationSettings;
		public static readonly Metric.Client.IRecorder Metrics;


		static Program()
		{
			Program.ConfigurationSettings = new ConfigurationSettings();
#if NETCORE
			var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json");
			var configurationRoot = builder.Build();
			configurationRoot.GetSection("configurationsettings").Bind(Program.ConfigurationSettings);
#else
			Program.ConfigurationSettings.MetricsHostName = Program.GetConfigurationSettingsValue("metricsHostName");
			Program.ConfigurationSettings.MetricsPort = int.Parse(Program.GetConfigurationSettingsValue("metricsPort", "0"));
			Program.ConfigurationSettings.MetricsKeyPrefix = Program.GetConfigurationSettingsValue("metricsKeyPrefix");
			Program.ConfigurationSettings.MetricsKeySuffix = Program.GetConfigurationSettingsValue("metricsKeySuffix");


#endif
			Program.ConfigurationSettings.IsConfiguredForClient =
				(Program.ConfigurationSettings.MetricsHostName != null && Program.ConfigurationSettings.MetricsPort > 0);

			Program.Metrics = new Metric.Client.NullRecorder();
			if (Program.ConfigurationSettings.IsConfiguredForClient)
			{
				Program.Metrics = new Metric.Client.Recorder(
					new Metric.Client.StatsdPipe(
						Program.ConfigurationSettings.MetricsHostName,
						Program.ConfigurationSettings.MetricsPort),
						Program.ConfigurationSettings.MetricsKeyPrefix,
						Program.ConfigurationSettings.MetricsKeySuffix);
			}
		}


		public static void Run(
			bool doRunServer = true,
			bool doRunClient = true)
		{
			var _cancellationTokenSource = new CancellationTokenSource();

			if (doRunServer)
			{
				var _serverTask = new Task(
					() =>
						{
							var _listener = new Server.Server(
								Program.ConfigurationSettings.MetricsPort,
								new Metric.Inv.v3.Server.IStatisticsProcessor[] { new Metric.Inv.v3.Server.ConsoleWriterStatisticsProcessor() });
							_listener.Start();
						},
					_cancellationTokenSource.Token,
					TaskCreationOptions.LongRunning);
				_serverTask.Start();
			}

			if (doRunClient)
			{
				var _clientTask = new Task(
					() =>
						{
							while (true)
							{
								_cancellationTokenSource.Token.ThrowIfCancellationRequested();

								var _worker = new Worker();
								using (var _timer = Program.Metrics.StartTimer("Program.Work"))
								{
									_worker.DoWork();
								}
							}
						},
					_cancellationTokenSource.Token,
					TaskCreationOptions.LongRunning);
				_clientTask.Start();
			}

			var _lineRead = string.Empty;
			while (_lineRead != "x")
			{
				Console.WriteLine("Hit x to stop ");
				_lineRead = Console.ReadLine();
			}

			_cancellationTokenSource.Cancel();
		}


#if NETFULL
		private static string GetConfigurationSettingsValue(
			string key,
			string defaultedValue = null)
		{
			var _appSettings = ConfigurationManager.AppSettings;
			var _keyValue = _appSettings[key];

			return _keyValue ?? defaultedValue;
		}
#endif
	}
}
