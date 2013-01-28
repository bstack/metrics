using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Metrics.v3.Server
{
	public class Server
	{
		private readonly int c_port;
		private readonly IEnumerable<IStatisticsProcessor> c_processors;


		private UdpClient c_listener;


		public Server(
			int port,
			IEnumerable<IStatisticsProcessor> processors)
		{
			this.c_port = port;
			this.c_processors = processors;
		}


		public void Start()
		{
			this.c_listener = new UdpClient(this.c_port);
			var _IPEndPoint = new IPEndPoint(IPAddress.Any, this.c_port);

			while(true)
			{
				var _rawStatisticData = this.c_listener.Receive(ref _IPEndPoint);
				var _rawStatisticDataAsString = Encoding.Default.GetString(_rawStatisticData, 0, _rawStatisticData.Length);
				var _rawStatistic = new RawStatistic(_rawStatisticDataAsString);

				foreach (var _processor in this.c_processors) { _processor.Process(_rawStatistic); }
			}
		}
	}
}
