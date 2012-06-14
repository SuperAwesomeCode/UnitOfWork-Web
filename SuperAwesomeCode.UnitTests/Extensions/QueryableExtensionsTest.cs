using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperAwesomeCode.UnitTests.Extensions
{
	[TestClass()]
	public class QueryableExtensionsTest : BaseUnitTest
	{
		[TestMethod]
		public void ToDynamicListTest()
		{
			List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			var dynamic = list
				.AsQueryable()
				.ToDynamicList(i =>
				new
				{
					Number = i,
					NumberAsString = i.ToString(),
					Object = new object()
				});

			Assert.IsTrue(dynamic.All(i => i.Number is int));
			Assert.IsTrue(dynamic.All(i => i.NumberAsString is string));
			Assert.IsTrue(dynamic.All(i => i.Object is object));
		}
	}
}