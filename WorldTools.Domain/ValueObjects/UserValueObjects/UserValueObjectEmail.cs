using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectEmail
    {
        public string UserEmail { get; set; }

        public UserValueObjectEmail(string email)
        {
            UserEmail = email;
        }
    }
}
