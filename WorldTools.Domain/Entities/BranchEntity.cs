using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Domain.Entities
{
    public class BranchEntity
    {
        public Guid BranchId { get; set; }

        [Required(ErrorMessage = "El nombre de la sucursal es obligatorio.")]

        public BranchValueObjectName BranchName { get; set; }

        [Required(ErrorMessage = "La ubicación de la sucursal es obligatoria.")]

        public BranchValueObjectLocation BranchLocation { get; set; }

        public virtual List<ProductEntity> BranchProducts { get; set; }

        public virtual List<UserEntity> BranchEmployees { get; set; }

        public BranchEntity(BranchValueObjectName name, BranchValueObjectLocation location)
        {
            BranchName = name;
            BranchLocation = location;
        }

    }
}
