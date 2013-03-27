using System;
using System.Text.RegularExpressions;


namespace Metrics.v3.Server
{
	public class RawStatistic
	{
		private static Regex c_parsingRegex = new Regex(@"(?<key>.+):(?<value>.+)\|(?<typeIdentifier>[a-z]{1,2})(\|@(?<sampleRate>.*))?", RegexOptions.Compiled);


		public readonly StatisticType Type;
		public readonly string Key;
		public readonly double Value;
		public readonly double SampleRate;


		public RawStatistic(
			string seed)
		{
			var _match = RawStatistic.c_parsingRegex.Match(seed);
			if (!_match.Success) { throw new ApplicationException("Parse failure"); }

			var _typeIdentifier = _match.Groups["typeIdentifier"].Value;
			if (_typeIdentifier == "c")			{ this.Type = StatisticType.Counter; }
			else if (_typeIdentifier == "g")	{ this.Type = StatisticType.Gauge; }
			else if (_typeIdentifier == "ms")	{ this.Type = StatisticType.Timer; }
			else								{ throw new ApplicationException("Unknown statistic type"); }
			
			this.Key = _match.Groups["key"].Value;

			this.Value = double.Parse(_match.Groups["value"].Value);

			this.SampleRate = double.Parse("0" + _match.Groups["sampleRate"].Value);
		}
	}
}
