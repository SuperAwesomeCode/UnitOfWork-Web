using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Transactions;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>A class used to orchestrate a group of ObjectContexts.</summary>
	internal sealed class ObjectContextOrchestrator : IObjectContextOrchestrator
	{
		/// <summary>Dictionary of ObjectContext(es).</summary>
		private Dictionary<EntityConnectionContainer, ObjectContext> _dictionary;

		/// <summary>Initializes a new instance of the <see cref="ObjectContextOrchestrator"/> class.</summary>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		public ObjectContextOrchestrator(IEnumerable<EntityConnectionContainer> entityConnectionContainers)
		{
			this._dictionary = new Dictionary<EntityConnectionContainer, ObjectContext>();
			foreach (var container in entityConnectionContainers)
			{
				this._dictionary.Add(container, null);
			}
		}

		/// <summary>Saves all of the Contexts in a TransactionScopr.</summary>
		public void Save()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				var values = this._dictionary.Values.Where(i => i != null).ToList();
				values.ForEach(i => i.SaveChanges(SaveOptions.AcceptAllChangesAfterSave));
				values.ForEach(i => i.AcceptAllChanges());

				scope.Complete();
			}
		}

		/// <summary>Gets the ObjectContext for the given type.</summary>
		/// <typeparam name="TEntityType">The type of the entity.</typeparam>
		/// <returns></returns>
		public ObjectContext GetObjectContext<TEntityType>()
		{
			string entityTypeNamespace = typeof(TEntityType).Namespace;

			//This is SingleOrDefault() because if there is a namespace collision it should fail.
			var key = this._dictionary.Keys
				.Where(i => string.Equals(i.ObjectContextType.Namespace, entityTypeNamespace))
				.SingleOrDefault();

			if (key == null)
			{
				return null;
			}

			if (this._dictionary[key] == null)
			{
				this._dictionary[key] = key.GetObjectContext();
			}

			return this._dictionary[key];
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose()
		{
			foreach (var keyValue in this._dictionary)
			{
				if (keyValue.Value != null)
				{
					keyValue.Value.Dispose();
				}
			}
		}
	}
}
