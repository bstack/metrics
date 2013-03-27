using System;


namespace Metric.Inv.v2
{
	public interface IRecorderTimerCompletion
	{
		void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0);
	}
}
