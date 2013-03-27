using System;


namespace Metrics.v3.Client
{
	public interface ITimingCompletionRecorder
	{
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);
	}
}
