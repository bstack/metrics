using System;


namespace Metric.Client
{
	public interface IRecorder
	{
		void Increment(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0);

		
		void Decrement(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0);
		
		
		void Gauge(
			string key,
			long value,
			double sampleRate = 1.0);
		
		
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);


		IDisposable StartTimer(
			string key);
	}
}
