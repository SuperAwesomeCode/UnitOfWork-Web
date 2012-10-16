using System;
using System.Collections.Generic;
using System.Linq;
using SuperAwesomeCode.DataModel;

namespace SuperAwesomeCode.UnitTests.DataModel
{
	public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		public MockRepository()
		{
			this.Collection = new List<TEntity>();
		}

		public List<TEntity> Collection { get; set; }

		public IQueryable<TEntity> Queryable
		{
			get { return this.Collection.AsQueryable(); }
		}

		public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> findExpression)
		{
			return this.Queryable.Where(findExpression).SingleOrDefault();
		}

		public void Add(TEntity entity)
		{
			this.Collection.Add(entity);
		}

		public void Remove(TEntity entity)
		{
			this.Collection.Remove(entity);
		}
	}
}
