﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ResponseVm.User
{
    public class AuthResponse
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
    }
}
