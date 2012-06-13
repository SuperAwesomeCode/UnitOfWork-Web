using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Extension class for PropertyInfo.</summary>
	public static class PropertyInfoExtensions
	{
		/// <summary>Sets the value.</summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <param name="obj">The object to set the value on.</param>
		/// <param name="value">The value to set.</param>
		public static void SetValue(this PropertyInfo propertyInfo, object obj, string value)
		{
			var propertyType = propertyInfo.PropertyType;

			if (propertyType.IsEnum)
			{
				var enumValue = EnumExtensions.ToEnum(propertyType, value);
				propertyInfo.SetValue(obj, enumValue, null);
				return;
			}

			if (propertyType.IsNullableT())
			{
				//If the proerty is nullable and the value is null or empty, do not set it.
				if (string.IsNullOrEmpty(value))
				{
					return;
				}

				propertyType = Nullable.GetUnderlyingType(propertyType);
			}

			MethodInfo methodInfo = propertyType.GetMethod("TryParse", new Type[] { typeof(string), propertyType.MakeByRefType() });
			if (methodInfo == null)
			{
				propertyInfo.SetValue(obj, value, null);
				return;
			}

			//If the value is nullable and it fails to parse, this will set the default value.
			object[] arguments = { value, null };
			methodInfo.Invoke(null, arguments);
			propertyInfo.SetValue(obj, arguments[1], null);
		}
	}
}
