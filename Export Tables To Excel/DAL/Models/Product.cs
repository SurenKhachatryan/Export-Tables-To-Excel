using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Product
    {
        public Product()
        {
            Sale = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }
    }
}
