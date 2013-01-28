using System;


namespace Metrics.v3
{
	public class NullRecorder : IRecorder, ITimingCompletionRecorder
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
			ulong value,
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


		public Timer Timing(
			string key)
		{
			return new Timer(this, key);
		}
	}
}
