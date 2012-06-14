using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class EnumerableExtensionsTest : BaseUnitTest
	{
		[TestMethod]
		public void ToDelimitedStringTest()
		{
			List<int> list = new List<int> { 0, 1, 2, 3, 4, 5 };
			Assert.AreEqual(string.Join(",", list), list.ToDelimitedString(","));
		}

		[TestMethod]
		public void ToDelimitedStringWithNullValuesTest()
		{
			List<int?> list = new List<int?> { 0, 1, 2, null, 4, 5 };
			Assert.AreEqual(string.Join(",", list), list.ToDelimitedString(","));
		}

		[TestMethod]
		public void ToDelimitedStringDoesNotFailWithNullEnumerableTest()
		{
			List<int?> list = null;
			Assert.AreEqual(string.Empty, list.ToDelimitedString(","));
		}		
	}
}
