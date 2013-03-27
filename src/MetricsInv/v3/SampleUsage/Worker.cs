using System;
using System.Threading;


namespace Metrics.v3.SampleUsage
{
	public class Worker
	{
		public void DoWork()
		{
			Program.Metrics.Increment("Worker.DoWork.Enter");
			Thread.Sleep(new Random().Next(5000));
			Program.Metrics.Increment("Worker.DoWork.Exiting", 1, .81);
		}
	}
}
