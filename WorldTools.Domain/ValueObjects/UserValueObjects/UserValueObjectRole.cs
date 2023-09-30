using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectRole
    {
        public enum roles
        {
            Super_admin,
            Admin,
            Employee
        }
    }
}
