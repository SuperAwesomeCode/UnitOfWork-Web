using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class PropertyInfoExtensionsTest : BaseUnitTest
	{
		[DataContract]
		private enum PropertyInfoEnum
		{
			Value1,

			[EnumMember(Value = "Contract Value")]
			Value2,

			Value3
		}

		[TestMethod]
		public void SetValueGuidTest()
		{
			Guid guid = Guid.NewGuid();
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("Guid");
			guidProperty.SetValue(testObject, guid.ToSafeString());

			Assert.AreEqual(guid, testObject.Guid);
		}

		[TestMethod]
		public void SetValueNullableGuidTest()
		{
			Guid guid = Guid.NewGuid();
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("NullableGuid");
			guidProperty.SetValue(testObject, guid.ToSafeString());

			Assert.AreEqual(guid, testObject.NullableGuid);
		}

		[TestMethod]
		public void SetValueNullableGuidThatIsNullTest()
		{
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("NullableGuid");
			guidProperty.SetValue(testObject, null);

			Assert.AreEqual(null, testObject.NullableGuid);
		}

		[TestMethod]
		public void SetValueIntegerTest()
		{
			int value = 100;
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("Integer");
			guidProperty.SetValue(testObject, value.ToString());

			Assert.AreEqual(value, testObject.Integer);
		}

		[TestMethod]
		public void SetValueStringTest()
		{
			string value = "SomeString";
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("String");
			guidProperty.SetValue(testObject, value);

			Assert.AreEqual(value, testObject.String);
		}

		[TestMethod]
		public void SetValueEnumTest()
		{
			PropertyInfoEnum value = PropertyInfoEnum.Value2;
			PropertyInfoTestObject testObject = new PropertyInfoTestObject();

			var guidProperty = typeof(PropertyInfoTestObject).GetProperty("Enum");
			guidProperty.SetValue(testObject, value.ToAttributeValue());

			Assert.AreEqual(value, testObject.Enum);
		}

		private class PropertyInfoTestObject
		{
			public Guid Guid { get; set; }

			public Guid? NullableGuid { get; set; }

			public PropertyInfoEnum Enum { get; set; }

			public int Integer { get; set; }

			public string String { get; set; }
		}
	}
}