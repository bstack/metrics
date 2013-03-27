using System;


namespace Metrics.v2
{
	public interface IRecorderTimerCompletion
	{
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);
	}
}
