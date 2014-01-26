using System;
using System.Threading;


namespace Metric.Inv.v3.SampleUsage
{
	public class Worker
	{
		public void DoWork()
		{
			Program.Metrics.Increment("Worker.DoWork.Enter");
			
			var _randomNumber = new Random().Next(5000);
			Thread.Sleep(_randomNumber);

			Program.Metrics.Gauge("Worker.SleepInterval", _randomNumber);
			Program.Metrics.Gauge("Worker.SleepInterval.Other", _randomNumber + 34);

			Program.Metrics.Timing("TOther.1", TimeSpan.FromMilliseconds(_randomNumber + 3422));

			Program.Metrics.Set("Worker.MySet", _randomNumber);
			Program.Metrics.Set("Worker.MySet", _randomNumber);
			

			Program.Metrics.Increment("Worker.DoWork.Exiting", 1, .81);
		}
	}
}
