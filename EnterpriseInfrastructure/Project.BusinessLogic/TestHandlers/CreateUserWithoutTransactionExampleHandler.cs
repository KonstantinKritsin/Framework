using System.Threading.Tasks;
using Project.DataAccess.Database1.Contracts;
using Project.Framework.BusinessLogic;

namespace Project.BusinessLogic.TestHandlers
{
	public class CreateUserWithoutTransactionExampleHandler : HandlerBase
	{
		public async Task Handle()
		{
			var repo = GetRepository<IUserRepository>();
			var user = repo.Create("test_login", "БургасБилет", "test@test.ru");

			repo.Insert(user);
			await CommitAsync();
		}
	}
}