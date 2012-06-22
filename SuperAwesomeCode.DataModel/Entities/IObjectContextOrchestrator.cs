using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Transactions;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>Interface used to orcestrate an ObjectContext.</summary>
	public interface IObjectContextOrchestrator : IDisposable
	{
		/// <summary>Saves all of the Contexts in a TransactionScopr.</summary>
		void Save();

		/// <summary>Gets the ObjectContext for the given type.</summary>
		/// <typeparam name="TEntityType">The type of the entity.</typeparam>
		/// <returns></returns>
		ObjectContext GetObjectContext<TEntityType>();
	}
}
