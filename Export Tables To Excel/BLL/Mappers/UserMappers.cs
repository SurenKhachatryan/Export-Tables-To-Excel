using BLL.ExcelModels;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Mappers
{
    public static class UserMappers
    {
        public static UserExcelModel MapToUserExcelModel(this User model)
        {
            if (model == null)
                return null;

            return new UserExcelModel()
            {
                Id = model.Id,
                Name = model.Name,
                Age = model.Age
            };
        }

        public static List<UserExcelModel> MapToUserExcelModels(this List<User> models)
        {
            if (models != null)
                return models.Select(MapToUserExcelModel).ToList();

            return new List<UserExcelModel>();
        }
    }
}
