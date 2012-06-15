using System;
using System.Collections.Generic;
using System.Linq;
using SuperAwesomeCode.DataModel;

namespace SuperAwesomeCode.UnitTests.DataModel
{
	public class MockUnitOfWork : IUnitOfWork
	{
		private Dictionary<Type, object> _repositories;

		public MockUnitOfWork()
			: this(null)
		{
		}

		public MockUnitOfWork(Dictionary<Type, object> repositories)
		{
			this._repositories = repositories ?? new Dictionary<Type, object>();
		}

		public bool HasRepository<TEntityType>() where TEntityType : class
		{
			return this._repositories.Keys.Any(i => i is TEntityType);
		}

		public IRepository<TEntityType> Repository<TEntityType>() where TEntityType : class
		{
			Type type = typeof(TEntityType);
			if (!this._repositories.ContainsKey(type))
			{
				this._repositories.Add(type, new MockRepository<TEntityType>());
			}

			return this._repositories[type] as MockRepository<TEntityType>;
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
