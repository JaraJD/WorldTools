
using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ValueObjects.SaleValueObjects
{
    public class SaleValueObjectNumber
    {
        [Required(ErrorMessage = "El número es requerido.")]
        [Range(1, 9999, ErrorMessage = "El número debe estar entre 1 y 9999.")]
        public int Number { get; set; }

        public SaleValueObjectNumber(int number)
        {
            if (number < 1 || number > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "El número debe estar entre 1 y 9999.");
            }

            Number = number;
        }


    }
}
