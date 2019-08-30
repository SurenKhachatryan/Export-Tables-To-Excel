using BLL.ExcelModels;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Mappers
{
    public static class SaleMappers
    {
        public static SaleExcelModel MapToSaleExcelModel(this Sale model)
        {
            if (model == null)
                return null;

            return new SaleExcelModel()
            {
                Id = model.Id,
                Price = model.Price,
                Discount = model.Discount,
                ProductId = model.ProductId
            };
        }

        public static List<SaleExcelModel> MapToSaleExcelModels(this List<Sale> models)
        {
            if (models != null)
                return models.Select(MapToSaleExcelModel).ToList();

            return new List<SaleExcelModel>();
        }
    }
}
