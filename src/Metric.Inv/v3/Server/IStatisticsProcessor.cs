using System;


namespace Metrics.v3.Server
{
	public interface IStatisticsProcessor
	{
		void Process(
			RawStatistic statistic);
	}
}
