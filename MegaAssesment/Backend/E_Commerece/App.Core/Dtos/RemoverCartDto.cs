using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class RemoverCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
