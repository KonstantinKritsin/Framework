namespace Project.Framework.DataAccess.Data
{
	public interface IRepository
	{
		void SetUnitOfWork(IDbUnitOfWork unitOfWork);
	}
}
