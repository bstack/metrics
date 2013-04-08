using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace Metric.Inv.v3.SampleUsage
{
	public class Program
	{
		public static readonly Configuration Configuration;
		public static readonly Metric.Client.IRecorder Metrics;


		static Program()
		{
			Program.Configuration = new Configuration();

			Program.Metrics = new Metric.Client.NullRecorder();
			if (Program.Configuration.IsConfiguredForClient)
			{
				Program.Metrics = new Metric.Client.Recorder(
					new Metric.Client.StatsdPipe(Program.Configuration.HostName, Program.Configuration.Port),
					Program.Configuration.KeyPrefix,
					Program.Configuration.KeySuffix);
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
								Program.Configuration.Port,
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
	}
}
