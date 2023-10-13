using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.Ports
{
    public interface IWebSocketPort
    {
        Task SendObjectToClient(string data);
    }
}
