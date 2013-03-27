using System;
using System.Threading;


namespace Metric.Inv.v1
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
