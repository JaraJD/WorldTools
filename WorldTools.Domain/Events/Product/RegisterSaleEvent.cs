
namespace WorldTools.Domain.Events.Product
{
    public class RegisterSaleEvent
    {
        public RegisterSaleEvent(string type, int quantity, Guid productId, Guid branchId)
        {
            Type = type;
            Quantity = quantity;
            ProductId = productId;
            BranchId = branchId;
        }

        public string Type { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        public Guid ProductId { get; set; }

        public Guid BranchId { get; set; }

        
    }
}
