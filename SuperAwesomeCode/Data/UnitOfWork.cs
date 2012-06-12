using System;
using System.Collections.Generic;
using System.Data.Objects;
using SuperAwesomeCode.Security;

namespace SuperAwesomeCode.Data
{
	/// <summary>Class which Implements of IUnitOfWork for FarmersDataContext.</summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>FarmersDataContext for this unit of work.</summary>
		private BatchedDataContext _BatchedDataContext;

		/// <summary>Dictionary of Repositories.</summary>
		private Dictionary<Type, object> _Repositories;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="batchedDataContext">The batched data context.</param>
		public UnitOfWork(BatchedDataContext batchedDataContext)
		{
			Guard.AgainstNull(batchedDataContext);
			this._BatchedDataContext = batchedDataContext;
			this._Repositories = new Dictionary<Type, object>();
		}

		/// <summary>
		/// Determines whether the TEntityType has repository.
		/// </summary>
		/// <typeparam name="TEntityType">TEntityType to see if a RepositryExists.</typeparam>
		/// <returns></returns>
		public bool HasRepository<TEntityType>() where TEntityType : class
		{
			return this._BatchedDataContext.GetObjectContext<TEntityType>() != null;
		}

		/// <summary>
		/// Retrieves a Repository for the given type.
		/// </summary>
		/// <typeparam name="TEntityType">TEntityType for the Repository to retrieve.</typeparam>
		/// <returns></returns>
		public IRepository<TEntityType> Repository<TEntityType>() where TEntityType : class
		{
			if (!this._Repositories.ContainsKey(typeof(TEntityType)))
			{
				this._Repositories.Add(typeof(TEntityType), new GenericRepository<TEntityType>(this._BatchedDataContext.GetObjectContext<TEntityType>()));
			}

			return this._Repositories[typeof(TEntityType)] as GenericRepository<TEntityType>;
		}

		/// <summary>
		/// Saves the unit of work.
		/// </summary>
		public void Save()
		{
			this._BatchedDataContext.Save();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (this._BatchedDataContext != null)
			{
				this._BatchedDataContext.Dispose();
			}
		}
	}
}
