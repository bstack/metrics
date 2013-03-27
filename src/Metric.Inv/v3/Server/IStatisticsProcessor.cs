using System;


namespace Metric.Inv.v3.Server
{
	public interface IStatisticsProcessor
	{
		void Process(
			RawStatistic statistic);
	}
}
