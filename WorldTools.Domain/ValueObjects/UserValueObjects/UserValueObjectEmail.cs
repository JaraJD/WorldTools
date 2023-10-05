using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectEmail
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")] public string UserEmail { get; set; }

        public UserValueObjectEmail(string email)
        {
            UserEmail = email;
        }


    }
}
