
using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ValueObjects.UserValueObjects
{
    public class UserValueObjectName
    {
        public UserValueObjectName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
        public string LastName { get; set; }

        
    }
}
