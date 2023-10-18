
namespace WorldTools.Domain.Ports
{
    public interface IWebSocketPort
    {
        Task<string> CreateProduct(Object message);

        Task<string> UpdateStock(Object message);

        Task<string> ProductSale(Object message);

        Task<string> UpdateSales(Object message);

    }
}
