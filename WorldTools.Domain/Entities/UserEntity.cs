using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public UserValueObjectName Name { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public UserValueObjectPassword UserPassword { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public UserValueObjectEmail Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public UserValueObjectRole Role { get; set; }

        [Required(ErrorMessage = "El BranchId es obligatorio.")]
        public Guid BranchId { get; set; }

        public virtual BranchEntity BranchEntity { get; set; }

        public UserEntity(UserValueObjectName name, UserValueObjectPassword password, UserValueObjectEmail email, UserValueObjectRole role, Guid branchId)
        {
            Name = name;
            UserPassword = password;
            Email = email;
            Role = role;
            BranchId = branchId;
        }
    }
}
