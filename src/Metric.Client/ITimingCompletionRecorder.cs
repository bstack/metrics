using System;


namespace Metric.Client
{
	public interface ITimingCompletionRecorder
	{
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);
	}
}
