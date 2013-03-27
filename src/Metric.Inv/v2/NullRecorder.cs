using System;


namespace Metric.Inv.v2
{
	public class NullRecorder : IRecorder, IRecorderTimerCompletion
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


		public RecorderTimer Timing(
			string key)
		{
			return new RecorderTimer(this, key);
		}
	}
}
