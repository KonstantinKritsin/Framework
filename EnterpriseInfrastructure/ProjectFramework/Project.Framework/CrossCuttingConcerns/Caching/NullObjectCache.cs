using System;

namespace Project.Framework.CrossCuttingConcerns.Caching
{
	public class NullObjectCache : ICache
	{
		public void Add(string key, object value, DateTime absoluteExpiration)
		{
		}

		public void Add(string key, object value, DateTimeOffset absoluteExpiration)
		{
		}

		public void Add(string key, object value, TimeSpan slidingExpiration)
		{
		}


		public bool TryGetValue<T>(string key, out object value)
		{
			value = null;
			return false;
		}
		public bool TryGetValue<T>(string key, out T value) where T : class
		{
			value = null;
			return false;
		}

		public void Remove(string key)
		{
		}

		public void ClearCache()
		{
		}

		public void Dispose()
		{
			
		}
	}
}