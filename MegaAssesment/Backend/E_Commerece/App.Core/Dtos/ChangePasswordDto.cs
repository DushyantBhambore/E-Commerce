using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class ChangePasswordDto
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
    }
}
