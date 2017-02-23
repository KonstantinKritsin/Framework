using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global

namespace Project.DataAccess.EntityFramework
{
    public static class QueryableExtensionsWrapper
	{
		[DbFunction("Edm", "DiffDays")]
		public static int? DiffDays(this DateTime? from, DateTime? to)
		{
			return DbFunctions.DiffDays(from, to);
		}

		[DbFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(this DateTimeOffset? date)
		{
			return DbFunctions.TruncateTime(date);
		}

		public static IQueryable<TSource> Include<TSource, TProperty>(this IQueryable<TSource> source, Expression<Func<TSource, TProperty>> path)
		{
			return QueryableExtensions.Include(source, path);
		}

		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.CountAsync(source, cancellationToken);
		}

		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.CountAsync(source, predicate, cancellationToken);
		}

		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.FirstOrDefaultAsync(source, cancellationToken);
		}

		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.FirstOrDefaultAsync(source, predicate, cancellationToken);
		}

		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.FirstAsync(source, cancellationToken);
		}

		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.FirstAsync(source, predicate, cancellationToken);
		}

		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SingleOrDefaultAsync(source, predicate, cancellationToken);
		}

		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SingleOrDefaultAsync(source, cancellationToken);
		}

		public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AllAsync(source, predicate, cancellationToken);
		}

		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AnyAsync(source, predicate, cancellationToken);
		}

		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AnyAsync(source, cancellationToken);
		}

		#region AverageAsync

		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, int>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, int?>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, long>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, long?>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, float>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, float?>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, double>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, double?>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, decimal>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source,
			Expression<Func<TSource, decimal?>> predicate, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, predicate, cancellationToken);
		}

		public static Task<double> AverageAsync(this IQueryable<int> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<double?> AverageAsync(this IQueryable<int?> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<double> AverageAsync(this IQueryable<long> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<double?> AverageAsync(this IQueryable<long?> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<float> AverageAsync(this IQueryable<float> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<float?> AverageAsync(this IQueryable<float?> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<double> AverageAsync(this IQueryable<double> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<double?> AverageAsync(this IQueryable<double?> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<decimal> AverageAsync(this IQueryable<decimal> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.AverageAsync(source, cancellationToken);
		}

		#endregion
		
		#region SumAsync

		public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<int> SumAsync(this IQueryable<int> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<int?> SumAsync(this IQueryable<int?> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<long> SumAsync(this IQueryable<long> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<long?> SumAsync(this IQueryable<long?> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<float> SumAsync(this IQueryable<float> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<float?> SumAsync(this IQueryable<float?> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<double> SumAsync(this IQueryable<double> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<double?> SumAsync(this IQueryable<double?> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, selector, cancellationToken);
		}
		public static Task<decimal> SumAsync(this IQueryable<decimal> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SumAsync(source, cancellationToken);
		}

		#endregion

		public static Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> source, TSource item,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ContainsAsync(source, item, cancellationToken);
		}

		public static Task ForEachAsync<TSource>(this IQueryable<TSource> source, Action<TSource> action,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ForEachAsync(source, action, cancellationToken);
		}

		public static IQueryable<TSource> AsNoTracking<TSource>(this IQueryable<TSource> source) where TSource : class
		{
			return QueryableExtensions.AsNoTracking(source);
		}

		public static IQueryable AsNoTracking(this IQueryable source)
		{
			return QueryableExtensions.AsNoTracking(source);
		}

		public static Task LoadAsync(this IQueryable source, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.LoadAsync(source, cancellationToken);
		}

		public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.MaxAsync(source, selector, cancellationToken);
		}

		public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.MaxAsync(source, cancellationToken);
		}

		public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.MinAsync(source, selector, cancellationToken);
		}

		public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.MinAsync(source, cancellationToken);
		}

		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SingleAsync(source, predicate, cancellationToken);
		}

		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.SingleAsync(source, cancellationToken);
		}


		public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ToArrayAsync(source, cancellationToken);
		}

		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySel, CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ToDictionaryAsync(source, keySel, cancellationToken);
		}
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySel, Func<TSource, TElement> elemSel,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ToDictionaryAsync(source, keySel, elemSel, cancellationToken);
		}

		public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source,
			CancellationToken cancellationToken = new CancellationToken())
		{
			return QueryableExtensions.ToListAsync(source, cancellationToken);
		}
	}
}
