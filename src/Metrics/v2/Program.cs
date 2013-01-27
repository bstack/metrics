﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace Metrics.v2
{
	public class Program
	{
		// Toggle based on config
		//public static IRecorder Metrics = new Recorder(
		//	new StatsdPipe("127.0.0.1", 30000),
		//	"MyProduct.v2",
		//	"TheApp");
		public static IRecorder Metrics = new NullRecorder();
		
		
		public static void Run()
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

						using (var _timer = Program.Metrics.Timing("Program.Work"))
						{
							_worker.DoWork(_sleep);
						}
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
