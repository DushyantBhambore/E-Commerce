using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseModel
{
    public class InvoiceResponseModel
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
        public object SalesDetails { get; set; }

        public InvoiceResponseModel(int statusCode, string message, object data, object salesDetails)
        {
            this.StatusCode = statusCode;
            this.message = message;
            this.Data = data;
            SalesDetails = salesDetails;
        }
    }
}
