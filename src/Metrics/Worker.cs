using System;
using System.Threading;


namespace Metrics
{
	public class Worker
	{
		public void DoWork(
			TimeSpan sleep)
		{
			Program.Metrics.Increment("Worker.DoWork.Enter");
			Thread.Sleep(sleep);
			Program.Metrics.Increment("Worker.DoWork.Exiting");
		}
	}
}
