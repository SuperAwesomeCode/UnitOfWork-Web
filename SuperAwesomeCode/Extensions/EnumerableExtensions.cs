using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>
	/// 	Extension methods for IEnumerable.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>Returns a delimited string of the enumerables.</summary>
		/// <typeparam name="T">Tpye of the enumerable.</typeparam>
		/// <param name="enumerable">The enumerable.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <returns></returns>
		public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, string delimiter)
		{
			return enumerable == null ?
				string.Empty :
				string.Join(delimiter, enumerable.Select(i => i.ToSafeString()));
		}
	}
}