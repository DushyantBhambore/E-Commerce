using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SaleQty { get; set; }
        public float SellingPrice { get; set; }
    }
}
