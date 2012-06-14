using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperAwesomeCode;
using SuperAwesomeCode.DataModel;
using SuperAwesomeCode.DataModel.Entities;
using TestApp.Entities;

namespace TestApp
{
	internal class Program
	{
		//TODO: Add test to UnitTests project. I have Mocked objects already, just not checked in.
		// Also IoC as a static may cause an issue when dealing with different application types.
		private static void Main(string[] args)
		{
			//Setup
			var container = EntityConnectionContainer.Create<TestEntityContainer>(
					@"System.Data.SqlClient",
					@".\SQLEXPRESS",
					@"|DataDirectory|\Entities\TestDatabase.mdf",
					@"Entities.TestEntityContainer");

			var module = new EntityDataApplicationModule(container);
			IoC ioc = new IoC(modules: module);

			try
			{
				//Save Example
				using (var unitOfWork = ioc.Resolve<IUnitOfWork>())
				{
					var userRepository = unitOfWork.Repository<User>();
					userRepository.Add(new User
					{
						Name = "Trownt",
						Created = DateTime.Now
					});

					var users = userRepository
					.Queryable
					.ToList();

					unitOfWork.Save();
				}

				//Query Example
				using (var unitOfWork = ioc.Resolve<IUnitOfWork>())
				{
					var userRepository = unitOfWork.Repository<User>();
					var users = userRepository
						.Queryable
						.ToList();

					Assert.IsTrue(users.Any());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);

				Thread.Sleep(5000);
			}
			finally
			{
				if (ioc != null)
				{
					ioc.Dispose();
				}
			}
		}
	}
}