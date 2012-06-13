using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Extension class for Type.</summary>
	public static class TypeExtensions
	{
		/// <summary>Gets the data contract properties.</summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> GetDataContractProperties(this Type type)
		{
			return type
			  .GetProperties()
			  .Where(i => i.GetCustomAttributes(false).FirstOrDefault(a => a is DataMemberAttribute) != null);
		}

		/// <summary>Gets the data contractt property names.</summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static IEnumerable<string> GetDataContractPropertyNames(this Type type)
		{
			return type.GetProperties()
				.Select(i =>
				{
					var attribute = i.GetCustomAttributes(false).Select(a => a as DataMemberAttribute).FirstOrDefault(a => a != null);
					if (attribute == null)
					{
						return null;
					}

					return string.IsNullOrEmpty(attribute.Name) ? i.Name : attribute.Name;
				})
				.Where(i => i != null);
		}

		/// <summary>Determines whether [is subclass of raw generic] [the specified type].</summary>
		/// <param name="type">The type.</param>
		/// <param name="genericType">Type of the generic.</param>
		/// <returns><c>true</c> if [is subclass of raw generic] [the specified type]; otherwise, <c>false</c>.</returns>
		public static bool IsSubclassOfRawGeneric(this Type type, Type genericType)
		{
			while (type != typeof(object) && type.IsClass)
			{
				var genericTypeDefinition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
				if (genericType == genericTypeDefinition)
				{
					return true;
				}

				type = type.BaseType;
			}

			return false;
		}
	}
}
