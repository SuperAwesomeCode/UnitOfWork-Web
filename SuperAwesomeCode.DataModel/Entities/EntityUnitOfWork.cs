using System;
using System.Collections.Generic;
using System.Data.Objects;
using SuperAwesomeCode.Security;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>Class which Implements of IUnitOfWork for Entities repositories.</summary>
	internal sealed class EntityUnitOfWork : IUnitOfWork
	{
		/// <summary>BatchedEntityDataContext for this unit of work.</summary>
		private BatchedEntityDataContext _batchedEntityDataContext;

		/// <summary>Dictionary of Repositories.</summary>
		private Dictionary<Type, object> _repositories;

		/// <summary>Initializes a new instance of the <see cref="EntityUnitOfWork"/> class.</summary>
		/// <param name="batchedDataContext">The batched data context.</param>
		public EntityUnitOfWork(BatchedEntityDataContext batchedDataContext)
		{
			Guard.AgainstNull(batchedDataContext);
			this._batchedEntityDataContext = batchedDataContext;
			this._repositories = new Dictionary<Type, object>();
		}

		/// <summary>Determines whether the TEntityType has repository.</summary>
		/// <typeparam name="TEntityType">TEntityType to see if a RepositryExists.</typeparam>
		/// <returns></returns>
		public bool HasRepository<TEntityType>() where TEntityType : class
		{
			return this._batchedEntityDataContext.GetObjectContext<TEntityType>() != null;
		}

		/// <summary>Retrieves a Repository for the given type.</summary>
		/// <typeparam name="TEntityType">TEntityType for the Repository to retrieve.</typeparam>
		/// <returns></returns>
		public IRepository<TEntityType> Repository<TEntityType>() where TEntityType : class
		{
			if (!this._repositories.ContainsKey(typeof(TEntityType)))
			{
				this._repositories.Add(typeof(TEntityType), new GenericEntityRepository<TEntityType>(this._batchedEntityDataContext.GetObjectContext<TEntityType>()));
			}

			return this._repositories[typeof(TEntityType)] as GenericEntityRepository<TEntityType>;
		}

		/// <summary>Saves the unit of work.</summary>
		public void Save()
		{
			this._batchedEntityDataContext.Save();
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose()
		{
			if (this._batchedEntityDataContext != null)
			{
				this._batchedEntityDataContext.Dispose();
			}
		}
	}
}
