using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RTMonSystem.Web.Client.Hubs
{
    public class YahooFinHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}