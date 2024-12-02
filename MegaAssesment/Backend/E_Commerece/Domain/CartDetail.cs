using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartDetail
    {
        [Key]
        public int CardDetailId { get; set; }

        // Master Cart Table PK 
        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public CartMaster CartMaster { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Qty { get; set; }
    }
}
