using System;
using System.Configuration;


namespace Metrics.v3
{
	public class Configuration
	{
		public readonly string HostName;
		public readonly int Port;
		public readonly bool IsConfiguredForClient;


		public Configuration ()
		{
			var _hostName = this.GetAppSettingsConfigurationValue("metricsHostName");
			var _port = int.Parse(this.GetAppSettingsConfigurationValue("metricsPort", "0"));

			this.HostName = _hostName;
			this.Port= _port;

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
