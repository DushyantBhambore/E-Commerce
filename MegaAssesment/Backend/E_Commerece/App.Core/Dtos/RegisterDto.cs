﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dtos
{
    public class RegisterDto
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
        public int RoleId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
    }
}
