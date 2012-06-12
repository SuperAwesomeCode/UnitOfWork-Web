using System;
using System.Data.Objects;
using System.Transactions;
using SuperAwesomeCode.Security;
using System.Collections.Generic;
using System.Linq;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>A classed used to manage all of the data contexts.</summary>
	public class BatchedEntityDataContext : IDisposable
	{
		//private List<Type> _ObjectContextTypes;
		private Dictionary<Type, ObjectContext> _Dictionary;

		/// <summary>
		/// Saves all of the Contexts in a TransactionScopr.
		/// </summary>
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

		/// <summary>
		/// Gets the ObjectContext for the given type.
		/// </summary>
		/// <typeparam name="T">Type which is used to determine the ObjectContext.</typeparam>
		/// <returns></returns>
		public ObjectContext GetObjectContext<T>()
		{
			Type type = typeof(T);
			var keyValue = this._Dictionary.Where(i => string.Equals(i.Key.Namespace, type.Namespace)).FirstOrDefault();
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

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchedEntityDataContext"/> class.
		/// </summary>
		/// <param name="objectContextsTypes">The object contexts types.</param>
		public BatchedEntityDataContext(IEnumerable<Type> objectContextsTypes)
		{
			this._Dictionary = new Dictionary<Type, ObjectContext>();
			foreach (var type in objectContextsTypes)
			{
				this._Dictionary.Add(type, null);
			}
		}
	}
}
