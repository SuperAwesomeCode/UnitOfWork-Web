using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
	/// <summary>Extension methods for string.</summary>
	public static class StringExtensions
	{
		/// <summary>Converts the string to an integer or a default value.</summary>
		/// <param name="value">String value to convert.</param>
		/// <param name="defaultValue">Default value to use if the parse fails.</param>
		/// <returns>Parsed integer or defaultValue.</returns>
		public static int ToIntOrDefault(this string value, int defaultValue = 0)
		{
			return (int)value.ToDecimalOrDefault(defaultValue);
		}

		/// <summary>Converts the string to an float or a default value.</summary>
		/// <param name="value">String value to convert.</param>
		/// <param name="defaultValue">Default value to use if the parse fails.</param>
		/// <returns>Parsed float or defaultValue.</returns>
		public static float ToFloatOrDefault(this string value, float defaultValue = 0)
		{
			return (float)value.ToDecimalOrDefault((decimal)defaultValue);
		}

		/// <summary>Converts the string to an double or a default value.</summary>
		/// <param name="value">String value to convert.</param>
		/// <param name="defaultValue">Default value to use if the parse fails.</param>
		/// <returns>Parsed double or defaultValue.</returns>
		public static double ToDoubleOrDefault(this string value, double defaultValue = 0)
		{
			return (double)value.ToDecimalOrDefault((decimal)defaultValue);
		}

		/// <summary>Converts the string to an decimal or a default value.</summary>
		/// <param name="value">String value to convert.</param>
		/// <param name="defaultValue">Default value to use if the parse fails.</param>
		/// <returns>Parsed decimal or defaultValue.</returns>
		public static decimal ToDecimalOrDefault(this string value, decimal defaultValue = 0)
		{
			decimal parsedValue = 0;
			return decimal.TryParse(value, out parsedValue) ? parsedValue : defaultValue;
		}

		/// <summary>Converts to nullable bool including using Y/N/string.Empty.</summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool? ConvertToNullableBool(this string value)
		{
			bool boolValue = false;
			if (bool.TryParse(value, out boolValue))
			{
				return (bool?)boolValue;
			}

			if (string.Equals(value, "y", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			if (string.Equals(value, "n", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			return null;
		}

		/// <summary>Safely splits the string value.</summary>
		/// <param name="value">The value.</param>
		/// <param name="delimiters">The delimiters.</param>
		/// <returns></returns>
		public static IEnumerable<string> SafeSplit(this string value, params string[] delimiters)
		{
			return string.IsNullOrWhiteSpace(value) ?
				new List<string>() :
				value
				.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
				.Where(i => !string.IsNullOrWhiteSpace(i)) //Above line doesn't remove whitespace only.
				.ToList();
		}

		/// <summary>Converts a string to a dateTime with the given format and kind.</summary>
		/// <param name="dateTimeString">The date time string.</param>
		/// <param name="dateTimeFormat">The date time format.</param>
		/// <param name="dateTimeKind">Kind of the date time.</param>
		/// <returns></returns>
		public static DateTime ToDateTime(this string dateTimeString, string dateTimeFormat, DateTimeKind dateTimeKind)
		{
			if (string.IsNullOrEmpty(dateTimeString))
			{
				return DateTime.MinValue;
			}

			DateTime dateTime;
			try
			{
				dateTime = DateTime.SpecifyKind(DateTime.ParseExact(dateTimeString, dateTimeFormat, CultureInfo.InvariantCulture), dateTimeKind);
			}
			catch (FormatException)
			{
				dateTime = DateTime.MinValue;
			}

			return dateTime;
		}

		/// <summary>Parses a string and converts to an enumeration.</summary>
		/// <typeparam name="T">Type of enumeration to return.</typeparam>
		/// <param name="fieldValue">String value.</param>
		/// <returns>Enumeration of the correct value.</returns>
		public static T ToEnum<T>(this string fieldValue) where T : struct, IConvertible
		{
			return EnumExtensions.ToEnum<T>(fieldValue);
		}

		/// <summary>Parses a string and converts to an enumeration.</summary>
		/// <typeparam name="T">Type of enumeration to return.</typeparam>
		/// <param name="fieldValue">String value.</param>
		/// <param name="defaultValue">Default value to use if the EnumMemberAttribute is not obtained.</param>
		/// <returns>Enumeration of the correct value.</returns>
		public static T ToEnum<T>(this string fieldValue, T defaultValue) where T : struct, IConvertible
		{
			return EnumExtensions.ToEnum<T>(fieldValue, defaultValue);
		}

		/// <summary>Splits and Trims a string value.</summary>
		/// <param name="value">Value to split and trim.</param>
		/// <param name="delimiters">Delimters to split by.</param>
		/// <returns>Enumeration of strings.</returns>
		public static IEnumerable<string> SplitAndTrim(this string value, params string[] delimiters)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new List<string>();
			}

			return value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
					.ToList()
					.Select(i => i.Trim())
					.Where(i => !string.IsNullOrWhiteSpace(i));
		}
	}
}