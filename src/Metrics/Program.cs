using System;


namespace Metrics
{
	class Program
	{
		static void Main(
			string[] args)
		{
			if (args.Length == 0) { args = new[] { "client and server" }; }

			Metrics.v3.Program.Run(
				(args.Length > 0 && args[0].IndexOf("server") >= 0),
				(args.Length > 0 && args[0].IndexOf("client") >= 0));
		}
	}
}
