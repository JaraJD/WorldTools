using Microsoft.AspNetCore.SignalR;

namespace WorldTools.WebSocketAdapter.Service
{
    public class WebSocketService : Hub
    {
        public async Task SendObjectToProducts(Object data)
        {
            await Clients.All.SendAsync("productsUpdate", data);
        }

        public async Task SendProductStockToUpdate(Object data)
        {
            await Clients.All.SendAsync("stockUpdate", data);
        }

        public async Task SendProductTosale(Object data)
        {
            await Clients.All.SendAsync("productSale", data);
        }

        public async Task SendObjectToSale(Object data)
        {
            await Clients.All.SendAsync("saleUpdate", data);
        }
    }
}
