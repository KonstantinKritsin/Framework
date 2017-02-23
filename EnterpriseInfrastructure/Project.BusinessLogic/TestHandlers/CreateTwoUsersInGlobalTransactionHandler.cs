using System.Threading.Tasks;
using Project.Framework.BusinessLogic;

namespace Project.BusinessLogic.TestHandlers
{
	public class CreateTwoUsersInGlobalTransactionHandler : HandlerBase
	{
		public async Task Handle()
		{
			using (GlobalTransaction())
			{
				await GetProcessor<CreateUserInTransactionExample>().ProcessAsync(h => h.Handle());
				await GetProcessor<CreateUserWithoutTransactionExampleHandler>().ProcessAsync(h => h.Handle());

				await CommitAsync();
			}
		}
	}
}