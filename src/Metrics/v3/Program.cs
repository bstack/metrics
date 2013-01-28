using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace Metrics.v3
{
	public class Program
	{
		public static readonly Configuration Configuration;
		public static readonly IRecorder Metrics;


		static Program()
		{
			Program.Configuration = new Configuration();

			Program.Metrics = new NullRecorder();
			if (Program.Configuration.IsConfiguredForClient)
			{
				Program.Metrics = new Recorder(
					new StatsdPipe(Program.Configuration.HostName, Program.Configuration.Port),
					"MyProduct.v2",
					"TheApp");
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
							var _listener = new Server(Program.Configuration.Port);
							_listener.Start();
						},
					_cancellationTokenSource.Token);
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

								var _sleep = TimeSpan.FromSeconds(2);
								var _worker = new Worker();

								using (var _timer = Program.Metrics.Timing("Program.Work"))
								{
									_worker.DoWork(_sleep);
								}
							}
						},
					_cancellationTokenSource.Token);
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
