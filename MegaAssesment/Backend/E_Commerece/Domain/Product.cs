using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product :AuditableEntity
    {
        [Key]
        public int ProdcutId { get; set; }

        public int UserId { get; set; }
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public string ProductImage { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public float SellingPrice { get; set; }

        public float PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Stock { get; set; }
    }
}
