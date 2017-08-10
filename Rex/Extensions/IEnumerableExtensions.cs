using System;
using System.Collections.Generic;

namespace Rex.Domain.Extensions
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}

		public static IEnumerable<T> AddItem<T>(this IEnumerable<T> enumeration, T value)
		{
			foreach (var current in enumeration)
			{
				yield return current;
			}

			yield return value;
		}
	}
}
