using BLL.Domains;
using BLL.Services.ExportExcelService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Export_Tables_To_Excel.Controllers
{
    [Route("api/ExportExcel")]
    [ApiController]
    public class ExportExcelController : ControllerBase
    {
        private readonly IExportExcelServices _exportExcelServices;

        public ExportExcelController(IExportExcelServices exportExcelServices)
        {
            _exportExcelServices = exportExcelServices;
        }


        [HttpPost]
        [Route("DownloadExcel")]
        public async Task<IActionResult> DownloadExcel(ExcelModel dwexcel)
        {
            var excel = await _exportExcelServices.GetExcel(dwexcel);

            if (excel.Excel == null || excel.Excel.Length == 0)
                return NotFound();

            return File(excel.Excel,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        string.Join("", excel.TabelName, ".xlsx"));

        }


    }
}