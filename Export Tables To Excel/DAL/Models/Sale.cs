using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
