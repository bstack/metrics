using System;


namespace Metric.Client
{
	public class Recorder : Metric.Client.IRecorder, Metric.Client.ITimingCompletionRecorder
	{
		private readonly Metric.Client.StatsdPipe c_pipe;
		private readonly string c_keyPrefix;
		private readonly string c_keySuffix;


		public Recorder(
			Metric.Client.StatsdPipe pipe,
			string keyPrefix = null,
			string keySuffix = null)
		{
			this.c_pipe = pipe;
			this.c_keyPrefix = keyPrefix;
			this.c_keySuffix = keySuffix;

			if (!string.IsNullOrWhiteSpace(this.c_keyPrefix))	{ this.c_keyPrefix += "."; }
			if (!string.IsNullOrWhiteSpace(this.c_keySuffix))	{ this.c_keySuffix = "." + this.c_keySuffix; }
		}


		public void Increment(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.c_pipe.Increment(this.ApplyKeyAffixes(key), magnitude, sampleRate);
		}


		public void Decrement(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.c_pipe.Decrement(this.ApplyKeyAffixes(key), magnitude, sampleRate);
		}


		public void Gauge(
			string key,
			ulong value,
			double sampleRate = 1.0)
		{
			this.c_pipe.Gauge(this.ApplyKeyAffixes(key), value, sampleRate);
		}


		public void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0)
		{
			this.c_pipe.Timing(this.ApplyKeyAffixes(key), value, sampleRate);
		}


		public IDisposable StartTimer(
			string key)
		{
			return new Metric.Client.Timer(this, key);
		}


		private string ApplyKeyAffixes(
			string key)
		{
			return this.c_keyPrefix  + key + this.c_keySuffix;
		}
	}
}
