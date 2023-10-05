using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectPassword
    {
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string UserPassword { get; set; }

        public UserValueObjectPassword(string password)
        {
            UserPassword = password;
        }
    }
}
