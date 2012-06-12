using System;
using SuperAwesomeCode.Security;

namespace SuperAwesomeCode.DataModel
{
	/// <summary>Interface for a unit of work against a collection of Repositories.</summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Determines whether the TEntityType has repository.
		/// </summary>
		/// <typeparam name="TEntityType">TEntityType to see if a RepositryExists.</typeparam>
		/// <returns></returns>
		bool HasRepository<TEntityType>() where TEntityType : class;

		/// <summary>
		/// Retrieves a Repository for the given type.
		/// </summary>
		/// <typeparam name="TEntityType">TEntityType for the Repository to retrieve.</typeparam>
		/// <returns></returns>
		IRepository<TEntityType> Repository<TEntityType>() where TEntityType : class;

		/// <summary>
		/// Saves the unit of work.
		/// </summary>
		void Save();
	}
}
