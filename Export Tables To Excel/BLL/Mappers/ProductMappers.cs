using BLL.ExcelModels;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Mappers
{
    public static class ProductMappers
    {
        public static ProductExcelModel MapToProductExcelModel(this Product model)
        {
            if (model == null)
                return null;

            return new ProductExcelModel()
            {
                Id = model.Id,
                Model = model.Model,
                Name = model.Name,
                ReleaseDate = model.ReleaseDate.ToString("dd.MM.yyyy")
            };
        }

        public static List<ProductExcelModel> MapToProductExcelModels(this List<Product> models)
        {
            if (models != null)
                return models.Select(MapToProductExcelModel).ToList();

            return new List<ProductExcelModel>();
        }
    }
}
