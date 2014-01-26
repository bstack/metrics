using System;


namespace Metric.Client
{
	public class NullRecorder : Metric.Client.IRecorder, Metric.Client.ITimingCompletionRecorder
	{
		public void Increment(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			// No op
		}


		public void Decrement(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			// No op
		}


		public void Gauge(
			string key,
			long value,
			double sampleRate = 1.0)
		{
			// No op
		}


		public void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0)
		{
			// No op
		}


		public void Set(
			string key,
			long value)
		{
			// No op
		}


		public IDisposable StartTimer(
			string key)
		{
			return new Metric.Client.Timer(this, key);
		}
	}
}
