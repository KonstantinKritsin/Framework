using System.Linq;

namespace Project.Framework.DataAccess.Data
{
	public interface IQueryRepository<out TEntity> : IRepository
		where TEntity : class
	{
		IQueryable<TEntity> Query();
	}
}