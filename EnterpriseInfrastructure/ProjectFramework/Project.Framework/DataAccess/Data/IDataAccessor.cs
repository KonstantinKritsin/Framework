using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.Framework.DataAccess.ModelContract;

namespace Project.Framework.DataAccess.Data
{
	public interface IDataAccessor<TEntity, TKey> : IQueryRepository<TEntity>
		where TEntity : class, IEntityKey<TKey>
		where TKey : IEquatable<TKey>
	{
		TEntity Find(TKey key);
		Task<TEntity> FindAsync(TKey key);
		void Insert(TEntity obj);
		TKey InsertIdentity(TEntity obj);
		void Update(TEntity obj);
		void Delete(TEntity obj);
		void Delete(TKey key);
		int Delete(Expression<Func<TEntity, bool>> predicate);
	}
}
