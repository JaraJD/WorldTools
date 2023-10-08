
namespace WorldTools.Domain.Events.Product
{
    public class RegisterStockEvent
    {
        public RegisterStockEvent(int quantity, Guid productId)
        {
            ProductQuantity = quantity;
            ProductId = productId;
        }

        public int ProductQuantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
