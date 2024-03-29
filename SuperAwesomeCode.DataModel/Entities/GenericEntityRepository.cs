﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>Implementation for a Generic Entity Repository.</summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	internal sealed class GenericEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		/// <summary>The ObjectContext containing the ObjectSet.</summary>
		private ObjectContext _objectContext;

		/// <summary>The ObjectSet for the entity.</summary>
		private ObjectSet<TEntity> _objectSet;

		/// <summary>Initializes a new instance of the <see cref="GenericEntityRepository&lt;TEntity&gt;"/> class.</summary>
		/// <param name="objectContext">The object context.</param>
		public GenericEntityRepository(ObjectContext objectContext)
		{
			this._objectContext = objectContext;
			this._objectSet = objectContext.CreateObjectSet<TEntity>();
		}

		/// <summary>Gets the for the Entity.</summary>
		public IQueryable<TEntity> Queryable
		{
			get { return this._objectSet.AsQueryable(); }
		}

		/// <summary>Finds the specific entity in the entity collection.</summary>
		/// <param name="findExpression">The find expression.</param>
		/// <returns>The Entity, null or an exception is thrown if multiple.</returns>
		public TEntity Find(Expression<Func<TEntity, bool>> findExpression)
		{
			return this._objectSet
				.Where(findExpression)
				.SingleOrDefault();
		}

		/// <summary>Adds the specified entity.</summary>
		/// <param name="entity">The entity.</param>
		public void Add(TEntity entity)
		{
			this._objectSet.AddObject(entity);
		}

		/// <summary>Removes the specified entity.</summary>
		/// <param name="entity">The entity.</param>
		public void Remove(TEntity entity)
		{
			this._objectSet.DeleteObject(entity);
		}
	}
}
