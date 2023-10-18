using Microsoft.AspNetCore.SignalR;
using WorldTools.Domain.Ports;

namespace WorldTools.WebSocketAdapter.Service
{
    public class WebSocketHandler : IWebSocketPort
    {
        private IHubContext<WebSocketService> _hubContext;

        public WebSocketHandler(IHubContext<WebSocketService> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<string> CreateProduct(Object message)
        {
            await _hubContext.Clients.All.SendAsync("createdProduct", message);
            return "ok";
        }

        public async Task<string> UpdateStock(Object message)
        {
            await _hubContext.Clients.All.SendAsync("stockUpdate", message);
            return "ok";
        }

        public async Task<string> ProductSale(Object message)
        {
            await _hubContext.Clients.All.SendAsync("soldProduct", message);
            return "ok";
        }

        public async Task<string> UpdateSales(Object message)
        {
            await _hubContext.Clients.All.SendAsync("updatedSales", message);
            return "ok";
        }

    }
}
