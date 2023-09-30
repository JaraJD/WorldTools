using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class RegisterProductData
    {
        public RegisterProductData(string? productName, string? productDescription, double productPrice, int productInventoryStock, ProductValueObjectCategory.Category productCategory, Guid branchId)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            ProductInventoryStock = productInventoryStock;
            ProductCategory = productCategory;
            BranchId = branchId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        [Required] public string? ProductName { get; set; }
        [Required] public string? ProductDescription { get; set; }
        [Required] public double ProductPrice { get; set; }
        [Required] public int ProductInventoryStock { get; set; }
        [Required] public ProductValueObjectCategory.Category ProductCategory { get; set; }
        [Required] public Guid BranchId { get; set; }

        [Required]
        [ForeignKey("BranchId")]
        public virtual RegisterBranchData Branch { get; set; }
    }
}
