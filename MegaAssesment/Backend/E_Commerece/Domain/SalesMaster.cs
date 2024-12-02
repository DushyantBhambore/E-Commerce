using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SalesMaster :AuditableEntity
    {
        [Key]
        public int SaleMasterId { get; set; }

        public string InvoiceId { get; set; }
        public int UserId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public float Subtotal { get; set; }

        public string DeliveryAddress { get; set; }

        public int DeliveryZipcode { get; set; }

        public int DeliveryState { get; set; }
        public int DeliveryCountry { get; set; }
    }
}
