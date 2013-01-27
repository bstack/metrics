using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace Metrics
{
	class Program
	{
		public static Metrics.IRecorder Metrics = new Metrics.Recorder(
			new Statsd.StatsdPipe("127.0.0.1", 30000),
			"MyProduct",
			"TheApp");
		
		
		static void Main(
			string[] args)
		{
			var _cancellationTokenSource = new CancellationTokenSource();

			var _serverTask = new Task(
				() =>
					{
						var _listener = new Server(30000);
						_listener.Start();
					},
					_cancellationTokenSource.Token);
			_serverTask.Start();

			var _clientTask = new Task(
				() =>
				{
					while (true)
					{
						_cancellationTokenSource.Token.ThrowIfCancellationRequested();

						var _sleep = TimeSpan.FromSeconds(2);
						var _worker = new Worker();

						var _stopWatch = Stopwatch.StartNew();
						_worker.DoWork(_sleep);
						_stopWatch.Stop();
						Program.Metrics.Timing("Program.Work", _stopWatch.Elapsed);
					}
				},
				_cancellationTokenSource.Token);
			_clientTask.Start();

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
