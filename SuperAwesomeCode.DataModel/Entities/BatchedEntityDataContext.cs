using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Transactions;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>A classed used to manage all of the data contexts.</summary>
	internal sealed class BatchedEntityDataContext : IDisposable
	{
		/// <summary>Dictionary of ObjectContext(es).</summary>
		private Dictionary<Type, ObjectContext> _Dictionary;

		/// <summary>Initializes a new instance of the <see cref="BatchedEntityDataContext"/> class.</summary>
		/// <param name="objectContextsTypes">The object contexts types.</param>
		public BatchedEntityDataContext(IEnumerable<Type> objectContextsTypes)
		{
			this._Dictionary = new Dictionary<Type, ObjectContext>();
			foreach (var type in objectContextsTypes)
			{
				if (!typeof(ObjectContext).IsAssignableFrom(type))
				{
					throw new ArgumentException(string.Format("{0} is not an ObjectContext.", type.FullName));
				}

				this._Dictionary.Add(type, null);
			}
		}

		/// <summary>Saves all of the Contexts in a TransactionScopr.</summary>
		public void Save()
		{
			using (TransactionScope scope = new TransactionScope())
			{
				var values = this._Dictionary.Values.Where(i => i != null).ToList();
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
			var keyValue = this._Dictionary.Where(i => string.Equals(i.Key.Namespace, entityTypeNamespace)).SingleOrDefault();
			if (keyValue.Key == null)
			{
				return null;
			}

			if (keyValue.Value == null)
			{
				this._Dictionary[keyValue.Key] = IoC.Resolve(keyValue.Key) as ObjectContext;
			}

			return this._Dictionary[keyValue.Key];
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose()
		{
			foreach (var keyValue in this._Dictionary)
			{
				if (keyValue.Value != null)
				{
					keyValue.Value.Dispose();
				}
			}
		}
	}
}
