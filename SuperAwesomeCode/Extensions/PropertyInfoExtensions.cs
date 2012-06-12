using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>
	/// Extension class for PropertyInfo.
	/// </summary>
	public static class PropertyInfoExtensions
	{
		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <param name="obj">The object to set the value on.</param>
		/// <param name="value">The value to set.</param>
		public static void SetValue(this PropertyInfo propertyInfo, object obj, string value)
		{
			//TODO: Refactor
			var propertyType = propertyInfo.PropertyType;

			if (propertyType == typeof(int))
			{
				propertyInfo.SetValue(obj, int.Parse(value), null);
			}
			else if (propertyType == typeof(int?))
			{
				int intValue = 0;
				if (int.TryParse(value, out intValue))
				{
					propertyInfo.SetValue(obj, intValue, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (int?)null, null);
				}
			}
			else if (propertyType == typeof(float))
			{
				propertyInfo.SetValue(obj, float.Parse(value), null);
			}
			else if (propertyType == typeof(float?))
			{
				float floatValue = 0;
				if (float.TryParse(value, out floatValue))
				{
					propertyInfo.SetValue(obj, floatValue, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (float?)null, null);
				}
			}
			else if (propertyType == typeof(double))
			{
				propertyInfo.SetValue(obj, float.Parse(value), null);
			}
			else if (propertyType == typeof(double?))
			{
				double doubleValue = 0;
				if (double.TryParse(value, out doubleValue))
				{
					propertyInfo.SetValue(obj, doubleValue, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (double?)null, null);
				}
			}
			else if (propertyType == typeof(decimal))
			{
				propertyInfo.SetValue(obj, decimal.Parse(value), null);
			}
			else if (propertyType == typeof(decimal?))
			{
				decimal decimalValue = 0;
				if (decimal.TryParse(value, out decimalValue))
				{
					propertyInfo.SetValue(obj, decimalValue, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (decimal?)null, null);
				}
			}
			else if (propertyType == typeof(bool))
			{
				propertyInfo.SetValue(obj, int.Parse(value), null);
			}
			else if (propertyType == typeof(bool?))
			{
				bool boolValue = false;
				if (bool.TryParse(value, out boolValue))
				{
					propertyInfo.SetValue(obj, boolValue, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (bool?)null, null);
				}
			}
			else if (propertyType == typeof(Guid))
			{
				propertyInfo.SetValue(obj, new Guid(value), null);
			}
			else if (propertyType == typeof(Guid?))
			{
				try
				{
					propertyInfo.SetValue(obj, new Guid(value), null);
				}
				catch
				{
					propertyInfo.SetValue(obj, (Guid?)null, null);
				}
			}
			else if (propertyType == typeof(DateTime))
			{
				//May need to support other date formats, but in ideal world the string should specific format
				propertyInfo.SetValue(obj, DateTime.Parse(value), null);
			}
			else if (propertyType == typeof(DateTime?))
			{
				DateTime dateTime = DateTime.MinValue;
				if (DateTime.TryParse(value, out dateTime))
				{
					propertyInfo.SetValue(obj, dateTime, null);
				}
				else
				{
					propertyInfo.SetValue(obj, (DateTime?)null, null);
				}
			}
			else if (propertyType.IsEnum)
			{
				var enumValue = EnumExtensions.ToEnum(propertyType, value);
				propertyInfo.SetValue(obj, enumValue, null);
			}
			else
			{
				propertyInfo.SetValue(obj, value, null);
			}
		}
	}
}
