using System;
using System.Linq;
using System.Threading;
using SuperAwesomeCode;
using SuperAwesomeCode.DataModel;
using SuperAwesomeCode.DataModel.Entities;
using TestApp.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp
{
	internal class Program
	{
		//TODO: Add test to UnitTests project. I have Mocked objects already, just not checked in.
		// Also IoC as a static may cause an issue when dealing with different application types.
		private static void Main(string[] args)
		{
			try
			{
				var container = EntityConnectionContainer.Create<TestEntityContainer>(
						@"System.Data.SqlClient",
						@".\SQLEXPRESS",
						@"|DataDirectory|\Entities\TestDatabase.mdf",
						@"Entities.TestEntityContainer");

				EntityDataApplication.Initialize(entityConnectionContainers: container);

				//Save Example
				using (var unitOfWork = IoC.Resolve<IUnitOfWork>())
				{
					var userRepository = unitOfWork.Repository<User>();
					userRepository.Add(new User
					{
						Name = "Trownt",
						Created = DateTime.Now
					});

					unitOfWork.Save();
				}
		
				//Query Example
				using (var unitOfWork = IoC.Resolve<IUnitOfWork>())
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
		}
	}
}