using BLL.Domains;
using BLL.Services.ExportExcelService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Export_Tables_To_Excel.Controllers
{
    [Route("api/DownloadExcel")]
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
        public async Task<IActionResult> DownloadExcel(DownloadExcel dwexcel)
        {
            var excel = await _exportExcelServices.ExportToExcel(dwexcel);

            if (excel.Excel == null || excel.Excel.Length == 0)
                return NotFound();

            return File(excel.Excel,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"{excel.TabelName}.xlsx");

        }


    }
}