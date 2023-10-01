using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectRole
    {
        public string Role { get; private set; }

        public UserValueObjectRole(string role)
        {
            validate(role);
        }

        private void validate(string role)
        {
            if ( role == null || role.Length < 3 )
            {
                throw new ArgumentNullException("Rol incorrecto");
            }

            Role = role;
        }

        public void SetValue(string role)
        {
            validate(role);
        }
    }
}
