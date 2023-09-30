using System.ComponentModel.DataAnnotations;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class ProductSaleData
    {
        [Required] public int ProductId { get; set; }

        [Required] public double ProductPrice { get; set; }

        [Required] public int ProductStock { get; set; }
    }
}
