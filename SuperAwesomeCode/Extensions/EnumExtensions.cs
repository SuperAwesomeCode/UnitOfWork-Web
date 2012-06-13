using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using SuperAwesomeCode;

namespace System
{
	/// <summary>Extension and static methods involving enumerations.</summary>
	public static class EnumExtensions
	{
		/// <summary>Obtains the EnumMemberAttribute.Value property or enumeration ToString().</summary>
		/// <typeparam name="T">Type of enumeration being passed in.</typeparam>
		/// <param name="resourceType">Value of the enumeration being passed in.</param>
		/// <returns>EnumMemberAttribute or resourceType.ToString().</returns>
		public static string ToAttributeValue<T>(this T resourceType) where T : struct, IConvertible
		{
			return EnumExtensions.ToAttributeValue(typeof(T), resourceType as Enum);
		}

		/// <summary>Obtains the EnumMemberAttribute.Value property or enumeration ToString().</summary>
		/// <param name="type">Type of enumeration being passed in.</param>
		/// <param name="field">Value of the enumeration being passed in.</param>
		/// <returns>EnumMemberAttribute or resourceType.ToString().</returns>
		public static string ToAttributeValue(Type type, Enum field)
		{
			Guard.AgainstNull(type);
			Guard.AgainstNull(field);

			if (!type.IsEnum)
			{
				throw new NotImplementedException();
			}

			EnumMemberAttribute attribute = EnumExtensions.GetEnumMemberAttribute(type, field.ToString());
			return attribute == null || string.IsNullOrEmpty(attribute.Value) ?
				field.ToString() :
				attribute.Value;
		}

		/// <summary>Parses a string and converts to an enumeration.</summary>
		/// <typeparam name="T">Type of enumeration to return.</typeparam>
		/// <param name="fieldValue">EnumMemberAttribute.Value string or enum.ToString().</param>
		/// <returns>Enumeration of the correct value.</returns>
		public static T ToEnum<T>(string fieldValue) where T : struct, IConvertible
		{
			return (T)(object)EnumExtensions.ToEnum(typeof(T), fieldValue);
		}

		/// <summary>Parses a string and converts to an enumeration.</summary>
		/// <param name="enumType">Type of enumeration to return.</param>
		/// <param name="fieldValue">EnumMemberAttribute.Value string or enum.ToString().</param>
		/// <returns>Enumeration of the correct value.</returns>
		public static Enum ToEnum(Type enumType, string fieldValue)
		{
			Enum enumValue = EnumExtensions.GetEnumFromAttribute(enumType, fieldValue);

			return enumValue == null ?
				(Enum)Enum.Parse(enumType, fieldValue) :
				enumValue;
		}

		/// <summary>Parses a string and converts to an enumeration.</summary>
		/// <typeparam name="T">Type of enumeration to return.</typeparam>
		/// <param name="fieldValue">EnumMemberAttribute.Value string or enum.ToString().</param>
		/// <param name="defaultValue">Default value to use if the EnumMemberAttribute is not obtained.</param>
		/// <returns>Enumeration of the correct value.</returns>
		public static T ToEnum<T>(string fieldValue, T defaultValue) where T : struct, IConvertible
		{
			Enum enumValue = EnumExtensions.GetEnumFromAttribute(typeof(T), fieldValue);
			if (enumValue != null)
			{
				return (T)(object)enumValue;
			}

			T parsedValue = defaultValue;
			return Enum.TryParse(fieldValue, out parsedValue) ? parsedValue : defaultValue;
		}

		/// <summary>Gets the enum from EnumMemberAttribute.</summary>
		/// <param name="enumType">Type of the enum.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns></returns>
		private static Enum GetEnumFromAttribute(Type enumType, string fieldValue)
		{
			if (!enumType.IsEnum)
			{
				throw new NotImplementedException();
			}

			foreach (var value in Enum.GetValues(enumType))
			{
				EnumMemberAttribute attribute = EnumExtensions.GetEnumMemberAttribute(enumType, value.ToString());
				if (attribute != null && string.Equals(attribute.Value, fieldValue))
				{
					return (Enum)value;
				}
			}

			return null;
		}

		/// <summary>Gets the first EnumMemberAttribute from a type using the passed in field name.</summary>
		/// <param name="type">Type to get the FieldInfo for.</param>
		/// <param name="field">Field to get the attribute from.</param>
		/// <returns>EnumMemberAttribute or null.</returns>
		private static EnumMemberAttribute GetEnumMemberAttribute(Type type, string field)
		{
			FieldInfo fieldInfo = type.GetField(field);
			return (fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[]).FirstOrDefault();
		}
	}
}