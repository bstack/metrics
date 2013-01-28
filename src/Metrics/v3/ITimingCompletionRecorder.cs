using System;


namespace Metrics.v3
{
	public interface ITimingCompletionRecorder
	{
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);
	}
}
