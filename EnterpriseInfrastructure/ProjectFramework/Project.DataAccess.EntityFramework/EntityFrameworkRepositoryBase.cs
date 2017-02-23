using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.Framework.DataAccess.Data;
using Project.Framework.DataAccess.ModelContract;

namespace Project.DataAccess.EntityFramework
{
	public abstract class EntityFrameworkDbContextRepositoryBase : IRepository
	{
		private readonly Type _contextType;
		protected DbContext Context;
		
		protected Func<bool> InTrunsaction { get; private set; } = () => false;

		protected EntityFrameworkDbContextRepositoryBase(Type contextType)
		{
			_contextType = contextType;
		}

		public void SetUnitOfWork(IDbUnitOfWork unitOfWork)
		{
			var efUof = (EntityFrameworkUnitOfWork)unitOfWork;
			Context = efUof.GetDbContext(_contextType);
			InTrunsaction = () => efUof.InTransaction() || efUof.InGlobalTransaction();
		}
	}

	public abstract class EntityFrameworkQueryRepository<TEntity> : EntityFrameworkDbContextRepositoryBase, IQueryRepository<TEntity> where TEntity : class
	{
		protected readonly Lazy<DbSet<TEntity>> DbSet;

		protected EntityFrameworkQueryRepository(Type contextType) : base(contextType)
		{
			DbSet = new Lazy<DbSet<TEntity>>(() => Context.Set<TEntity>());
		}

		public IQueryable<TEntity> Query()
		{
			return InTrunsaction() ? DbSet.Value : DbSet.Value.AsNoTracking();
		}
	}

	public class EntityFrameworkRepositoryBase<TEntity, TKey> : EntityFrameworkQueryRepository<TEntity>, IDataAccessor<TEntity, TKey>
		where TEntity : class, IEntityKey<TKey>
		where TKey : IEquatable<TKey>
	{
		// конструкция AsNoTracking при отключенных транзакциях убирает кэширование в ef. при частых повторяющихся выстрелах в базу и при наличии просадок из-за этого - можно будет убрать

		#region Ctor

		protected EntityFrameworkRepositoryBase(Type contextType) : base(contextType)
		{
			
		}

		#endregion

		#region Find

		public TEntity Find(TKey key)
		{
			return InTrunsaction() ? DbSet.Value.Find(key) : DbSet.Value.AsNoTracking().FirstOrDefault(e => e.Id.Equals(key));
		}

		public Task<TEntity> FindAsync(TKey key)
		{
			return InTrunsaction() ? DbSet.Value.FindAsync(key) : DbSet.Value.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(key));
		}

		#endregion

		#region Insert

		public void Insert(TEntity entity)
		{
			if (InTrunsaction()) DbSet.Value.Add(entity);
		}

		public TKey InsertIdentity(TEntity entity)
		{
			if (!InTrunsaction()) return default(TKey);

            var item = DbSet.Value.Add(entity);
            Context.SaveChanges();
	        return item.Id;
	    }

		#endregion

		#region Delete

		public void Delete(TEntity obj)
		{
			Delete(obj.Id);
		}

		public void Delete(TKey key)
		{
			Delete(entity => entity.Id.Equals(key));
		}

		public int Delete(Expression<Func<TEntity, bool>> predicate)
		{
			return InTrunsaction() ? DbSet.Value.RemoveRange(DbSet.Value.Where(predicate)).Count() : 0;
		}

		#endregion

		#region Update

		public void Update(TEntity entity)
		{
			if (InTrunsaction())
				Context.Entry(entity).State = Context.Entry(entity).State == EntityState.Added
					? EntityState.Added
					: EntityState.Modified;
		}

		#endregion
	}
}