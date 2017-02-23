using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.CrossCuttingConcerns.Caching
{
	public interface ICache : IDisposable
	{

        void Add(string key, object value, DateTimeOffset absoluteExpiration);
		void Add(string key, object value, TimeSpan slidingExpiration);

		bool TryGetValue<T>(string key, out object value);

		bool TryGetValue<T>(string key, out T value) where T : class;

		void Remove(string key);
		void ClearCache();
	}
}