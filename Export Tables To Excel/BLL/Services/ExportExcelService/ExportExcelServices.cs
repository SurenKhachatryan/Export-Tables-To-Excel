using BLL.Constants;
using BLL.Domains;
using BLL.Mappers;
using DAL.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.ExportExcelService
{
    public class ExportExcelServices : IExportExcelServices
    {
        private readonly StoreDBContext _db;

        public ExportExcelServices(StoreDBContext db)
        {
            _db = db;
        }

        public async Task<ExcelModel> GetExcel(ExcelModel excel)
        {
            await Task.CompletedTask;

            var byteArrExcel = default(byte[]);

            switch (excel.TabelName.ToLower())
            {
                case DBTablesConstants.Sales:
                    byteArrExcel = ExportToExcel(_db.Sale.ToList().MapToSaleExcelModels(), DBTablesConstants.Sales, "A1:D");
                    break;
                case DBTablesConstants.Products:
                    byteArrExcel = ExportToExcel(_db.Product.ToList().MapToProductExcelModels(), DBTablesConstants.Products, "A1:D");
                    break;
            }

            return new ExcelModel
            {
                Excel = byteArrExcel,
                TabelName = string.Join(".", excel.TabelName, DateTime.UtcNow.ToString("dd.MM.yyyy"))
            };
        }

        private byte[] ExportToExcel<TEntity>(List<TEntity> entities, string tableName, string range) where TEntity : class
        {
            var excel = default(byte[]);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(tableName);

                range = string.Join("", range, (entities.Count + 1));

                AddColumns<TEntity>(worksheet, range);

                worksheet.Cells["A2"].LoadFromCollection(entities);

                excel = package.GetAsByteArray();
            }

            return excel;
        }


        private void AddColumns<TEntity>(ExcelWorksheet worksheet, string range) where TEntity : class
        {
            var props = typeof(TEntity).GetProperties().ToList();

            using (var rng = worksheet.Cells[range])
            {
                var table = worksheet.Tables.Add(rng, "name");

                var index = -1;
                props.ForEach(x => table.Columns[++index].Name = x.Name);

                table.ShowFilter = true;
                table.ShowTotal = true;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }
        }
    }
}
