using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>
	/// 	Extension methods for IQueryables.
	/// </summary>
	public static class QueryableExtensions
	{
		/// <summary>Cast the Queryable as a dynamic of an anonymous. This allows retrieving dynamics from LINQ to Entities.</summary>
		/// <typeparam name="TQueryable">The type of the queryable.</typeparam>
		/// <typeparam name="TAnonymous">The type of the anonymous.</typeparam>
		/// <param name="queryable">The queryable.</param>
		/// <param name="findExpression">The find expression.</param>
		/// <returns></returns>
		public static IList<dynamic> ToDynamicList<TQueryable, TAnonymous>(
			this IQueryable<TQueryable> queryable,
			Expression<Func<TQueryable, TAnonymous>> findExpression)
		{
			return queryable
				.Select(findExpression) //Select the items (LINQ to Entities)
				.ToList() //To Annonymous List
				.Select(i => (dynamic)i) //Cast as Dynamic (LINQ to Objects)
				.ToList(); //To Dynamic List
		}
	}
}