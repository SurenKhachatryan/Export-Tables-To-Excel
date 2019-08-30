namespace BLL.ExcelModels
{
    public class SaleExcelModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
        public int? ProductId { get; set; }
    }
}
