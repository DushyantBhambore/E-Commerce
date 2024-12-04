using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class SalesMasterDto
    {
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public int DeliveryZipcode { get; set; }
        public int DeliveryState { get; set; }
        public int DeliveryCountry { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
