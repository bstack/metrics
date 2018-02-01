using System;


namespace Metric.Inv
{
	public class ConfigurationSettings
	{
		public string MetricsHostName { get; set; }
		public int MetricsPort { get; set; }
		public string MetricsKeyPrefix { get; set; }
		public string MetricsKeySuffix { get; set; }
		public bool IsConfiguredForClient { get; set; }
	}
}
