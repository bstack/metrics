using System;


namespace Metric.Client
{
	public class Recorder : Metric.Client.IRecorder, Metric.Client.ITimingCompletionRecorder
	{
		private readonly Metric.Client.StatsdPipe c_pipe;
		private readonly string c_keyPrefix;


		public Recorder(
			Metric.Client.StatsdPipe pipe,
			string keyPrefix)
		{
			this.c_pipe = pipe;
			this.c_keyPrefix = keyPrefix;

			if (this.c_keyPrefix != null) { this.c_keyPrefix += "."; }
		}


		public void Increment(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.c_pipe.Increment(this.ApplyKeyPrefix(key), magnitude, sampleRate);
		}


		public void Decrement(
			string key,
			ulong magnitude = 1,
			double sampleRate = 1.0)
		{
			this.c_pipe.Decrement(this.ApplyKeyPrefix(key), magnitude, sampleRate);
		}


		public void Gauge(
			string key,
			ulong value,
			double sampleRate = 1.0)
		{
			this.c_pipe.Gauge(this.ApplyKeyPrefix(key), value, sampleRate);
		}


		public void Timing(
			string key,
			TimeSpan value,
			double sampleRate = 1.0)
		{
			this.c_pipe.Timing(this.ApplyKeyPrefix(key), value, sampleRate);
		}


		public IDisposable StartTimer(
			string key)
		{
			return new Metric.Client.Timer(this, key);
		}


		private string ApplyKeyPrefix(
			string key)
		{
			return this.c_keyPrefix  + key;
		}
	}
}
