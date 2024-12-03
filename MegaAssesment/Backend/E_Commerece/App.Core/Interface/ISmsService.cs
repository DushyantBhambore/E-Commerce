﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interface
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string toPhoneNumber, string message);
    }
}
