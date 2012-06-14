using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class ObjectExtensionsTest : BaseUnitTest
	{
		[TestMethod]
		public void ToSafeStringTest()
		{
			int value = 12345;
			Assert.AreEqual(value.ToString(), value.ToSafeString());
		}

		[TestMethod]
		public void ToSafeStringDoesNotFailWhenNullTest()
		{
			int? value = null;
			Assert.AreEqual(string.Empty, value.ToSafeString());
		}
	}
}
