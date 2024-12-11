using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class ProdcutDto
    {
        public int ProdcutId { get; set; }
        public int UserId { get; set; }

        public string ProductName { get; set; }

        public IFormFile ProductImage { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public float SellingPrice { get; set; }

        public float PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Stock { get; set; }
    }
}
