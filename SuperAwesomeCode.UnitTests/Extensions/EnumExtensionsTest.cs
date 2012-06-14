using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class EnumExtensionsTest : BaseUnitTest
	{
		[DataContract]
		public enum TestEnumeration
		{
			[EnumMember(Value = "Value One")]
			Value1,

			[EnumMember]
			Value2,

			Value3
		}

		[TestMethod()]
		public void ToAttributeValueTest()
		{
			Assert.AreEqual(TestEnumeration.Value1.ToAttributeValue(), "Value One");
			Assert.AreEqual(TestEnumeration.Value2.ToAttributeValue(), "Value2");
			Assert.AreEqual(TestEnumeration.Value3.ToAttributeValue(), "Value3");
		}

		[TestMethod()]
		public void ToEnumTest()
		{
			//Calling EnumExtensions.ToEnum() directly as the string.ToEnum() should have its own tests.
			Assert.AreEqual(EnumExtensions.ToEnum<TestEnumeration>("Value One"), TestEnumeration.Value1);
			Assert.AreEqual(EnumExtensions.ToEnum<TestEnumeration>("Value2"), TestEnumeration.Value2);
			Assert.AreEqual(EnumExtensions.ToEnum<TestEnumeration>("Value3"), TestEnumeration.Value3);
			Assert.AreEqual(EnumExtensions.ToEnum<TestEnumeration>("Not_A_Value", TestEnumeration.Value2), TestEnumeration.Value2);
		}
	}
}
