using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class CartdetailsDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
