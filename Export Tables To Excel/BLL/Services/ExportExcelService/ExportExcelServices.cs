using BLL.Constants;
using BLL.Domains;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.ExportExcelService
{
    public class ExportExcelServices : IExportExcelServices
    {
        private readonly mydbContext _db;

        public ExportExcelServices(mydbContext db)
        {
            _db = db;
        }

        public async Task<DownloadExcel> ExportToExcel(DownloadExcel dwexcel)
        {
            await Task.CompletedTask;

            byte[] excel = default(byte[]);

            switch (dwexcel.TabelName)
            {
                case DBTablesConstants.Users:
                    excel = ExportDBTableToExcel(_db.User, DBTablesConstants.Users);
                    break;
            }

            return new DownloadExcel
            {
                Excel = excel,
                TabelName = $"{dwexcel.TabelName}.{DateTime.UtcNow.ToString("dd.MM.yyyy")}"
            };
        }

        private byte[] ExportDBTableToExcel<TEntity>(DbSet<TEntity> entities, string TableName) where TEntity : class
        {
            byte[] excel;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(TableName);

                AddColumns<TEntity>(worksheet);
                AddRowsValues(worksheet, entities);
                excel = package.GetAsByteArray();
            }

            return excel;
        }

        private void AddColumns<TEntity>(ExcelWorksheet worksheet) where TEntity : class
        {
            var properies = typeof(TEntity).GetProperties().ToList();

            for (int i = 1; i < (properies.Count + 1); i++)
            {
                worksheet.Cells[1, i].Value = properies[i - 1].Name;

                worksheet.Cells[1, i].Style.Font.Size = 12;
                worksheet.Cells[1, i].Style.Font.Bold = true;
                worksheet.Cells[1, i].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            }
        }


        private void AddRowsValues<TEntity>(ExcelWorksheet worksheet, DbSet<TEntity> entities) where TEntity : class
        {
            var lsEntities = entities.ToList();
            var rowIndex = 0;
            var startRow = 2;

            for (int i = startRow; i < (lsEntities.Count + startRow); i++)
            {
                foreach (var item in lsEntities[i - startRow].GetType().GetProperties())
                {
                    worksheet.Cells[i, ++rowIndex].Value = item.GetValue(lsEntities[i - startRow]);
                }
                rowIndex = 0;
            }
        }
    }
}
