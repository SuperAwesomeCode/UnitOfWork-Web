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
			Assert.AreEqual("Value One".ToEnum<TestEnumeration>(), TestEnumeration.Value1);
			Assert.AreEqual("Value2".ToEnum<TestEnumeration>(), TestEnumeration.Value2);
			Assert.AreEqual("Value3".ToEnum<TestEnumeration>(), TestEnumeration.Value3);
		}
	}
}
