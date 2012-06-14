using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class StringeExtensionsTest : BaseUnitTest
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

		[TestMethod]
		public void ToIntOrDefaultTest()
		{
			//Success Cases
			Assert.AreEqual(1, "1".ToIntOrDefault());
			Assert.AreEqual(2, "2".ToIntOrDefault());
			Assert.AreEqual(1, "1.2".ToIntOrDefault(2));

			//Parameter Default Cases
			Assert.AreEqual(1, "x".ToIntOrDefault(1));
			Assert.AreEqual(2, "x".ToIntOrDefault(2));

			//Default Case
			Assert.AreEqual(default(int), "x".ToIntOrDefault());
		}

		[TestMethod]
		public void ToFloatOrDefaultTest()
		{
			//Success Cases
			Assert.AreEqual(1f, "1".ToFloatOrDefault());
			Assert.AreEqual(1.2f, "1.2".ToFloatOrDefault());

			//Parameter Default Cases
			Assert.AreEqual(1, "x".ToFloatOrDefault(1));
			Assert.AreEqual(2.5f, "x".ToFloatOrDefault(2.5f));

			//Default Case
			Assert.AreEqual(default(float), "x".ToFloatOrDefault());
		}

		[TestMethod]
		public void ToDoubleOrDefault()
		{
			//Success Cases
			Assert.AreEqual(1d, "1".ToDoubleOrDefault());
			Assert.AreEqual(1.2d, "1.2".ToDoubleOrDefault());

			//Parameter Default Cases
			Assert.AreEqual(1d, "x".ToDoubleOrDefault(1d));
			Assert.AreEqual(2.5d, "x".ToDoubleOrDefault(2.5d));

			//Default Case
			Assert.AreEqual(default(double), "x".ToDoubleOrDefault());
		}

		[TestMethod]
		public void ToDecimalOrDefault()
		{
			//Success Cases
			Assert.AreEqual(1m, "1".ToDecimalOrDefault());
			Assert.AreEqual(1.2m, "1.2".ToDecimalOrDefault());

			//Parameter Default Cases
			Assert.AreEqual(1m, "x".ToDecimalOrDefault(1m));
			Assert.AreEqual(2.5m, "x".ToDecimalOrDefault(2.5m));

			//Default Case
			Assert.AreEqual(default(decimal), "x".ToDecimalOrDefault());
		}

		[TestMethod]
		public void ConvertToNullableBoolTest()
		{
			Assert.AreEqual((bool?)true, "true".ConvertToNullableBool());
			Assert.AreEqual((bool?)true, "TrUe".ConvertToNullableBool());
			Assert.AreEqual((bool?)false, "false".ConvertToNullableBool());
			Assert.AreEqual((bool?)false, "FaLse".ConvertToNullableBool());

			Assert.AreEqual((bool?)true, "y".ConvertToNullableBool());
			Assert.AreEqual((bool?)true, "Y".ConvertToNullableBool());
			Assert.AreEqual((bool?)false, "n".ConvertToNullableBool());
			Assert.AreEqual((bool?)false, "N".ConvertToNullableBool());

			Assert.AreEqual((bool?)null, "Bacon".ConvertToNullableBool());
		}

		[TestMethod]
		public void SafeSplitTest()
		{
			string value = "0,\r,1,2,,,\t,3,   ,4,5,\n,6,7,	,8,9";
			var list = value.SafeSplit(",").ToList();

			//Should be list of: 0,1,2,3,4,5,6,7,8,9
			Assert.IsTrue(list.Count == 10);
			for (int i = 0; i < list.Count; i++)
			{
				Assert.AreEqual(i.ToString(), list[i]);
			}
		}

		[TestMethod]
		public void ToDateTimeTest()
		{
			DateTime dateTime = DateTime.Now;
			dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

			Assert.AreEqual(dateTime, dateTime.ToString("yyyy.MM.dd-HH.mm.ss").ToDateTime("yyyy.MM.dd-HH.mm.ss", DateTimeKind.Local));
			Assert.AreEqual(dateTime, dateTime.ToString("MM/dd/yyyy HH:mm:ss").ToDateTime("MM/dd/yyyy HH:mm:ss", DateTimeKind.Utc));
			Assert.AreEqual(dateTime, dateTime.ToString("dd/MM/yyyy HH:mm:ss").ToDateTime("dd/MM/yyyy HH:mm:ss", DateTimeKind.Unspecified));
		}

		[TestMethod()]
		public void ToEnumStringExtensionTest()
		{
			Assert.AreEqual("Value One".ToEnum<TestEnumeration>(), TestEnumeration.Value1);
			Assert.AreEqual("Value2".ToEnum<TestEnumeration>(), TestEnumeration.Value2);
			Assert.AreEqual("Value3".ToEnum<TestEnumeration>(), TestEnumeration.Value3);
			Assert.AreEqual("Not_A_Value".ToEnum<TestEnumeration>(TestEnumeration.Value2), TestEnumeration.Value2);
		}
	}
}