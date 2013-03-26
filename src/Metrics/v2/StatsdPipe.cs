// Customised version of https://raw.github.com/etsy/statsd/master/examples/csharp_example.cs
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Text;


namespace Metrics.v2
{
	public class StatsdPipe
	{
		private readonly UdpClient c_udpClient;
		private readonly ThreadLocal<Random> c_random = new ThreadLocal<Random>(() => new Random());


		public StatsdPipe(
			string host,
			int port)
		{
			this.c_udpClient = new UdpClient(host, port);
		}


		public void Increment(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.Send(sampleRate, String.Format("{0}:{1}|c", key, magnitude));
		}


		public void Decrement(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.Send(sampleRate, String.Format("{0}:{1}|c", key, ((long)magnitude * -1)));
		}


		public void Gauge(
			string key,
			ulong value,
			double sampleRate = 1.0)
		{
			this.Send(sampleRate, String.Format("{0}:{1}|g", key, value));
		}


		public void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0)
		{
			this.Send(sampleRate, String.Format("{0}:{1}|ms", key, (long)value.TotalMilliseconds));
		}


		private void Send(
			double sampleRate,
			params string[] statistics)
		{
			// Need to investigate why the < 1.0 and random is used ?
			if (sampleRate < 1.0)
			{
				foreach (var _statistic in statistics)
				{
					if (this.c_random.Value.NextDouble() <= sampleRate)
					{
						this.DoSend(String.Format("{0}|@{1:f}", _statistic, sampleRate));
					}
				}
			}
			else
			{
				foreach (var _statistic in statistics)
				{
					this.DoSend(_statistic);
				}
			}
		}


		private void DoSend(
			string statistic)
		{
			var _statisticData = Encoding.Default.GetBytes(statistic + "\n");
			this.c_udpClient.Send(_statisticData, _statisticData.Length);
		}
	}
}