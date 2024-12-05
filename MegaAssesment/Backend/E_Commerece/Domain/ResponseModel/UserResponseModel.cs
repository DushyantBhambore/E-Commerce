using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseModel
{
    public class UserResponseModel
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
        public UserResponseModel(int statusCode, string message, object data)
        {
            this.StatusCode = statusCode;
            this.message = message;
            this.Data = data;
        }

    }


}
