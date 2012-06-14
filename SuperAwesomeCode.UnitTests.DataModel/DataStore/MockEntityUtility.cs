using SuperAwesomeCode.DataModel.Entities;

namespace SuperAwesomeCode.UnitTests.DataModel.DataStore
{
	public static class MockEntityUtility
	{
		public static EntityConnectionContainer RetrieveConnectionContainer()
		{
			return EntityConnectionContainer.Create<MockEntityContext>(
				@"System.Data.SqlClient",
				@".\SQLEXPRESS",
				@"|DataDirectory|\DataStore\MockDatabase.mdf",
				@"DataStore.MockEntityContext");
		}
	}
}
