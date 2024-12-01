using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public string ExpiryDate { get; set; }

        public string CVV { get; set; }
    }
}
