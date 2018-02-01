using System;


namespace Metric.Inv
{
	class Program
	{
		static void Main(
			string[] args)
		{
			if (args.Length == 0) { args = new[] { "server" }; }

			Metric.Inv.v3.SampleUsage.Program.Run(
				(args.Length > 0 && args[0].IndexOf("server") >= 0),
				(args.Length > 0 && args[0].IndexOf("client") >= 0));
		}
	}
}
