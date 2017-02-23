using System.Linq;
using System.Threading.Tasks;
using Project.DataAccess.Database1.Contracts;
using Project.Framework.BusinessLogic;

namespace Project.BusinessLogic.TestHandlers
{
	public class GetUserCountHandler : HandlerBase
	{
		public int Handle()
		{
			return GetRepository<IUserRepository>().Query().Count();
		}
	}
}