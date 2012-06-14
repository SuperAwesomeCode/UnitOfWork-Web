using System;
using System.Collections.Generic;
using System.Linq;
using SuperAwesomeCode.DataModel;

namespace SuperAwesomeCode.UnitTests.DataModel
{
	public class MockUnitOfWork : IUnitOfWork
	{
		private Dictionary<Type, object> _Repositories;

		public MockUnitOfWork()
			: this(null)
		{
		}

		public MockUnitOfWork(Dictionary<Type, object> repositories)
		{
			this._Repositories = repositories ?? new Dictionary<Type, object>();
		}

		public bool HasRepository<EntityType>() where EntityType : class
		{
			return this._Repositories.Keys.Any(i => i is EntityType);
		}

		public IRepository<EntityType> Repository<EntityType>() where EntityType : class
		{
			Type type = typeof(EntityType);
			if (!this._Repositories.ContainsKey(type))
			{
				this._Repositories.Add(type, new MockRepository<EntityType>());
			}

			return this._Repositories[type] as MockRepository<EntityType>;
		}

		public void Save()
		{
			return;
		}

		public void Dispose()
		{
			return;
		}
	}
}
