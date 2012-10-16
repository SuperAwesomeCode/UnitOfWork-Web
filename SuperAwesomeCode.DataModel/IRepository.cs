using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SuperAwesomeCode.DataModel
{
	/// <summary>Interface for a Repository.</summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IRepository<TEntity> where TEntity : class
	{
		/// <summary>Gets the for the Entity.</summary>
		IQueryable<TEntity> Queryable { get; }

		/// <summary>Finds the specific entity in the entity collection.</summary>
		/// <param name="findExpression">The find expression.</param>
		/// <returns>The Entity, null or an exception is thrown if multiple.</returns>
		TEntity Find(Expression<Func<TEntity, bool>> findExpression);

		/// <summary>Adds the specified entity.</summary>
		/// <param name="entity">The entity.</param>
		void Add(TEntity entity);

		/// <summary>Removes the specified entity.</summary>
		/// <param name="entity">The entity.</param>
		void Remove(TEntity entity);
	}
}
