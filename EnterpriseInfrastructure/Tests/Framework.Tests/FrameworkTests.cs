using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Project.BusinessLogic.TestHandlers;
using Project.Framework.BusinessLogic;

namespace Framework.Tests
{
    [TestClass]
    public class FrameworkTests : BaseTest
    {
        [TestMethod]
        public async Task CreateUserInTransaction()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateUserInTransactionExample>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 1, countNew);
        }

        [TestMethod]
        public async Task TryCreateUserWithoutTransaction()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateUserWithoutTransactionExampleHandler>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev, countNew);
        }

        [TestMethod]
        public async Task CreateTwoUsersInGlobalTransaction()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateTwoUsersInGlobalTransactionHandler>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 2, countNew);
        }

        [TestMethod]
        public async Task CreateTwoUsersInnerScope()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateTwoUsersInnerScopeHandler>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 2, countNew);
        }

        [TestMethod]
        public async Task CreateTwoUserInOnRequestButDifferentHandlers()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateUserInTransactionExample>().ProcessAsync(h => h.Handle());
            await handlerFactory.Get<CreateUserInTransactionExample>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 2, countNew);
        }

        [TestMethod]
        public async Task CreateTwoUsersInSeparateTransactions()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateTwoUsersInSeparateTransactionsHandler>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 2, countNew);
        }

        [TestMethod]
        public async Task CreateOneUserInnerScope()
        {
            var handlerFactory = Kernel.Get<HandlerProcessorFactory>();
            var countPrev = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            await handlerFactory.Get<CreateOneUserInnerScopeHandler>().ProcessAsync(h => h.Handle());
            var countNew = handlerFactory.Get<GetUserCountHandler>().Process(h => h.Handle());
            Assert.AreEqual(countPrev + 1, countNew);
        }
    }
}