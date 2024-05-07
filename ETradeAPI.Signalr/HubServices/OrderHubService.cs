using ETradeAPI.Application.Abstraction.Hubs;
using ETradeAPI.Signalr.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Signalr.HubServices
{
    public class OrderHubService:IOrderHubService
    {
        readonly IHubContext<OrderHub> _context;

        public OrderHubService(IHubContext<OrderHub> context)
        {
            _context = context;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
           await _context.Clients.All.SendAsync("receiveOrderAddedMessage",message);
        }
    }
}
