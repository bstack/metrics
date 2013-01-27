using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Metrics
{
	public class Server
	{
		private readonly int c_port;


		private UdpClient c_listener;


		public Server(
			int port)
		{
			this.c_port = port;
		}


		public void Start()
		{
			this.c_listener = new UdpClient(this.c_port);
			
			var _IPEndPoint = new IPEndPoint(IPAddress.Any, this.c_port);
			while(true)
			{
				var _receivedData = this.c_listener.Receive(ref _IPEndPoint);
				var _receivedDataAsString = Encoding.Default.GetString(_receivedData, 0, _receivedData.Length);
				Console.Write("\t{0}", _receivedDataAsString);
			}
		}
	}
}
