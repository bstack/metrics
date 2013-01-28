using System;


namespace Metrics.v3.Server
{
	public class ConsoleWriterStatisticsProcessor : IStatisticsProcessor
	{
		public void Process(
			RawStatistic statistic)
		{
			Console.WriteLine("\t{0} {1} {2}",
				statistic.Key,
				statistic.Type,
				statistic.Value);
		}
	}
}
