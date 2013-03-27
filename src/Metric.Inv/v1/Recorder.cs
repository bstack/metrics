using System;


namespace Metric.Inv.v1
{
	public class  Recorder : IRecorder
	{
		private readonly Statsd.StatsdPipe c_pipe;
		private readonly string c_keyPrefix;


		public Recorder(
			Statsd.StatsdPipe pipe,
			string productName,
			string applicationName)
		{
			this.c_pipe = pipe;

			this.c_keyPrefix = string.Format("{0}.{1}.", productName, applicationName);
		}


		public void Decrement(
			params string[] keys)
		{
			this.c_pipe.Decrement(this.ApplyKeyPrefix(keys));
		}


		public void Decrement(
			int magnitude,
			params string[] keys)
		{
			this.c_pipe.Decrement(magnitude, this.ApplyKeyPrefix(keys));
		}


		public void Decrement(
			int magnitude,
			double sampleRate,
			params string[] keys)
		{
			this.c_pipe.Decrement(magnitude, sampleRate, this.ApplyKeyPrefix(keys));
		}


		public void Decrement(
			string key)
		{
			this.c_pipe.Decrement(this.ApplyKeyPrefix(key));
		}


		public void Decrement(
			string key,
			int magnitude)
		{
			this.c_pipe.Decrement(this.ApplyKeyPrefix(key), magnitude);
		}


		public void Decrement(
			string key,
			int magnitude,
			double sampleRate)
		{
			this.c_pipe.Decrement(this.ApplyKeyPrefix(key), magnitude, sampleRate);
		}


		public void Gauge(
			string key,
			int value)
		{
			this.c_pipe.Gauge(this.ApplyKeyPrefix(key), value);
		}


		public void Gauge(
			string key,
			int value,
			double sampleRate)
		{
			this.c_pipe.Gauge(this.ApplyKeyPrefix(key), value, sampleRate);
		}


		public void Increment(
			int magnitude,
			double sampleRate,
			params string[] keys)
		{
			this.c_pipe.Increment(magnitude, sampleRate, this.ApplyKeyPrefix(keys));
		}


		public void Increment(
			string key)
		{
			this.c_pipe.Increment(this.ApplyKeyPrefix(key));
		}


		public void Increment(
			string key,
			int magnitude)
		{
			this.c_pipe.Increment(this.ApplyKeyPrefix(key), magnitude);

		}


		public void Increment(
			string key,
			int magnitude,
			double sampleRate)
		{
			this.c_pipe.Increment(this.ApplyKeyPrefix(key), magnitude, sampleRate);
		}


		public void Timing(
			string key,
			TimeSpan value)
		{
			this.c_pipe.Timing(this.ApplyKeyPrefix(key), (int)value.TotalMilliseconds);
		}


		public void Timing(
			string key,
			TimeSpan value,
			double sampleRate)
		{
			this.c_pipe.Timing(this.ApplyKeyPrefix(key), (int)value.TotalMilliseconds, sampleRate);
		}


		public void Dispose()
		{
			// Pending
		}


		private string[] ApplyKeyPrefix(
			string[] keys)
		{
			var _result = new string[keys.Length];
			for (var _index = 0; _index < keys.Length; _index++)
			{
				_result[_index] = this.ApplyKeyPrefix(keys[_index]);
			}
			return _result;
		}

	
		private string ApplyKeyPrefix(
			string key)
		{
			return this.c_keyPrefix + key; 
		}
	}
}
