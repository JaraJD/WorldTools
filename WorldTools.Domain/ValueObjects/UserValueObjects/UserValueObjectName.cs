﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectName
    {
        public string UserName { get; set; }

        public UserValueObjectName(string name)
        {
            UserName = name;
        }
    }
}
