using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
	/// <summary>Extension methods for object.</summary>
	public static class ObjectExtensions
	{
		/// <summary>Returns object.ToString() or string.Empty if null.</summary>
		/// <param name="value">The object value.</param>
		/// <returns></returns>
		public static string ToSafeString(this object value)
		{
			return value == null ? string.Empty : value.ToString();
		}
	}
}