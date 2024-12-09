using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class GetCartDto
    {
        public int CartDetailsId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public int CartId { get; set; }
        public float SellingPrice { get; set; }
        public float Total { get; set; }

    }
}
