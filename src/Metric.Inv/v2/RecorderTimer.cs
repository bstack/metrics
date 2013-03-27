using System;
using System.Diagnostics;


namespace Metric.Inv.v2
{
	public class RecorderTimer : IDisposable
	{
		private readonly IRecorderTimerCompletion c_recorderTimerCompletion;
		private readonly string c_key;
		private readonly Stopwatch c_stopWatch;


		private bool c_disposed;


		public RecorderTimer(
			IRecorderTimerCompletion recorderTimerCompletion,
			string key)
		{
			this.c_recorderTimerCompletion = recorderTimerCompletion;
			this.c_key = key;

			this.c_stopWatch = Stopwatch.StartNew();
		}


		public void Dispose()
		{
			if (!this.c_disposed)
			{
				this.c_disposed = true;

				this.c_stopWatch.Stop();
				this.c_recorderTimerCompletion.Timing(this.c_key, this.c_stopWatch.Elapsed);
			}
		}
	}
}