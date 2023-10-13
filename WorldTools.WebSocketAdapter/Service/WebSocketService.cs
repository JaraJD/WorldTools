using Microsoft.AspNetCore.SignalR;

namespace WorldTools.WebSocketAdapter.Service
{
    public class WebSocketService : Hub
    {
        public async Task SendObjectToProducts(Object data)
        {
            await Clients.All.SendAsync("productsUpdate", data);
        }

        public async Task SendObjectToSale(Object data)
        {
            await Clients.All.SendAsync("saleUpdate", data);
        }
    }
}
