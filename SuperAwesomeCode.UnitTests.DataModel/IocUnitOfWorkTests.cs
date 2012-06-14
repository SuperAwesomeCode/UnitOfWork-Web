using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeCode.DataModel;
using SuperAwesomeCode.DataModel.Entities;
using SuperAwesomeCode.UnitTests.DataModel.DataStore;

namespace SuperAwesomeCode.UnitTests.DataModel
{
	[TestClass()]
	public class IocUnitOfWorkTest : BaseUnitTest
	{
		/// <summary>This just ensures the contract between IoC and IUnitOfWork does not change.</summary>
		[TestMethod()]
		public void IocUnitOfWorkMockedTest()
		{
			//EXAMPLE: Shows how to set up a Mocked Repository

			//Set up facked repository
			var mockedUnitOfWork = new MockUnitOfWork();
			mockedUnitOfWork.Repository<Tuple<int, string>>().Add(new Tuple<int, string>(1, "First"));
			mockedUnitOfWork.Repository<Tuple<int, string>>().Add(new Tuple<int, string>(2, string.Empty));
			mockedUnitOfWork.Repository<Tuple<int, string>>().Add(new Tuple<int, string>(3, "Third"));

			//Call into IoC for IUnitOfWork and validate items came back.
			using (IoC ioc = new IoC(modules: new MockUnitOfWorkModule(mockedUnitOfWork)))
			using (var unitOfWork = ioc.Resolve<IUnitOfWork>())
			{
				var repository = unitOfWork.Repository<Tuple<int, string>>();
				var items = repository
					.Queryable
					.Where(i => !string.IsNullOrEmpty(i.Item2))
					.ToList();

				Assert.IsTrue(items.Any());
			}
		}

		[TestMethod]
		public void IocUnitOfWorkDataStoreTest()
		{
			//EXAMPLE: Shows how to setup an entity database connection.

			EntityConnectionContainer container = MockEntityUtility.RetrieveConnectionContainer();
			using (IoC ioc = new IoC(modules: new EntityDataModule(container)))
			using (var unitOfWork = ioc.Resolve<IUnitOfWork>())
			{
				var count = unitOfWork.Repository<User>().Queryable.Count();
				unitOfWork.Repository<User>().Add(new User { Name = "TestName", Created = DateTime.Now });
				unitOfWork.Save();

				Assert.IsTrue(count < unitOfWork.Repository<User>().Queryable.Count());
			}
		}
	}
}
