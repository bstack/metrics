using System;
using System.Diagnostics;


namespace Metrics.v3.Client
{
	public class Timer : IDisposable
	{
		private readonly ITimingCompletionRecorder c_timingCompletionRecorder;
		private readonly string c_key;
		private readonly Stopwatch c_stopWatch;


		private bool c_disposed;


		public Timer(
			ITimingCompletionRecorder timingCompletionRecorder,
			string key)
		{
			this.c_timingCompletionRecorder = timingCompletionRecorder;
			this.c_key = key;

			this.c_stopWatch = Stopwatch.StartNew();
		}


		public void Dispose()
		{
			if (!this.c_disposed)
			{
				this.c_disposed = true;

				this.c_stopWatch.Stop();
				this.c_timingCompletionRecorder.Timing(this.c_key, this.c_stopWatch.Elapsed);
			}
		}
	}
}