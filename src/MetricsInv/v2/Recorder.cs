using System;


namespace Metrics.v2
{
	public class Recorder : IRecorder, IRecorderTimerCompletion
	{
		private readonly StatsdPipe c_pipe;
		private readonly string c_keyPrefix;


		public Recorder(
			StatsdPipe pipe,
			string productName,
			string applicationName)
		{
			this.c_pipe = pipe;

			this.c_keyPrefix = string.Format("{0}.{1}.", productName, applicationName);
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


		public RecorderTimer Timing(
			string key)
		{
			return new RecorderTimer(this, key);
		}


		private string ApplyKeyPrefix(
			string key)
		{
			return this.c_keyPrefix + key; 
		}
	}
}
