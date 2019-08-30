using BLL.Domains;
using System.Threading.Tasks;

namespace BLL.Services.ExportExcelService
{
    public interface IExportExcelServices
    {
        Task<ExcelModel> GetExcel(ExcelModel excel);
    }
}
