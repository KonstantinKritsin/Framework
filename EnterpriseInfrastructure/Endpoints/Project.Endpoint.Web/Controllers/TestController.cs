using System.Web.Http;
using Project.BusinessLogic.UserHandlers;
using Project.Common.InternalContracts.Authentication;
using Project.Framework.BusinessLogic;

namespace Project.Endpoint.Web.Controllers
{
    public class TestController : ApiController
    {
        private readonly HandlerProcessorFactory _processorFactory;

        public TestController(HandlerProcessorFactory processorFactory)
        {
            _processorFactory = processorFactory;
        }

        public UserIdentityModel[] GetUsers()
        {
            return _processorFactory.Get<GetUsersHandler>().Process(h => h.Handle());
        }
    }
}
