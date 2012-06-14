using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeCode.DataModel;
using SuperAwesomeCode.DataModel.Entities;
using SuperAwesomeCode.UnitTests.DataModel.DataStore;

namespace SuperAwesomeCode.UnitTests.DataModel.Entities
{
	[TestClass]
	public class EntityConnectionContainerTest : BaseUnitTest
	{
		[TestMethod]
		public void EntityConnectionContainerOpenCloseTest()
		{
			EntityConnectionContainer container = MockEntityUtility.RetrieveConnectionContainer();
			var objectContext = container.GetObjectContext();

			objectContext.Connection.Open();
			Assert.AreEqual(System.Data.ConnectionState.Open, objectContext.Connection.State);

			objectContext.Connection.Close();
			Assert.AreEqual(System.Data.ConnectionState.Closed, objectContext.Connection.State);
		}
	}
}
