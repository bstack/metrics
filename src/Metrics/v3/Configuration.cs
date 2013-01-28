using System;
using System.Configuration;


namespace Metrics.v3
{
	public class Configuration
	{
		public readonly string HostName;
		public readonly int Port;
		public readonly string KeyPrefix;
		public readonly bool IsConfiguredForClient;


		public Configuration ()
		{
			this.HostName = this.GetAppSettingsConfigurationValue("metricsHostName");
			this.Port = int.Parse(this.GetAppSettingsConfigurationValue("metricsPort", "0"));
			this.KeyPrefix = this.GetAppSettingsConfigurationValue("metricsKeyPrefix");

			this.IsConfiguredForClient = (this.HostName != null && this.Port > 0);
		}


		private string GetAppSettingsConfigurationValue(
			string key,
			string defaultedValue = null)
		{
			var _appSettings = ConfigurationManager.AppSettings;
			var _keyValue = _appSettings[key];

			return _keyValue ?? defaultedValue;
		}
	}
}
