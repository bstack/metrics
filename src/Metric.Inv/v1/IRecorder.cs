using System;


namespace Metric.Inv.v1
{
	public interface IRecorder
	{
		void Decrement(
			params string[] keys);


		void Decrement(
			int magnitude,
			params string[] keys);


		void Decrement(
			int magnitude,
			double sampleRate,
			params string[] keys);


		void Decrement(
			string key);


		void Decrement(
			string key,
			int magnitude);


		void Decrement(
			string key,
			int magnitude,
			double sampleRate);
		

		void Dispose();


		void Gauge(
			string key,
			int value);


		void Gauge(
			string key,
			int value,
			double sampleRate);


		void Increment(
			int magnitude,
			double sampleRate,
			params string[] keys);


		void Increment(
			string key);


		void Increment(
			string key,
			int magnitude);


		void Increment(
			string key,
			int magnitude,
			double sampleRate);


		void Timing(
			string key,
			TimeSpan value);


		void Timing(
			string key,
			TimeSpan value,
			double sampleRate);
	}
}
