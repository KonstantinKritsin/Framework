using Project.Framework.Common.DependencyResolver;

namespace Project.Common.ExternalContracts.ExcelReportService
{
    public class ReportGeneratorFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        public ReportGeneratorFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public T GetReportGenerator<T>() where T : class, IReportGenerator
        {
            return _dependencyResolver.Get<T>();
        }
    }
}