using System.Threading.Tasks;
using Project.Framework.BusinessLogic;

namespace Project.BusinessLogic.TestHandlers
{
	public class CreateOneUserInnerScopeHandler : HandlerBase
	{
		public async Task Handle()
		{
			await GetProcessor<CreateUserInTransactionExample>().ProcessAsync(h => h.Handle());
			await GetProcessor<CreateUserWithoutTransactionExampleHandler>().ProcessAsync(h => h.Handle());

			await CommitAsync();
		}
	}
}