using System.IO;
using System.Threading.Tasks;

namespace Project.Common.ExternalContracts.ExcelReportService
{
    public interface IReportGenerator
    { }

    public interface IReportGenerator<in T> : IReportGenerator
    {
        Task GenerateTo(T model, Stream destination);
    }
}