
namespace WorldTools.Domain.Events.Product
{
    public class RegisterStockEvent
    {
        public RegisterStockEvent(int quantity, Guid productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
