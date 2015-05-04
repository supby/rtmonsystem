using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using RTMonSystem.DataSources.REST.Yahoo;
using RTMonSystem.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RTMonSystem.Web.Client.Hubs
{
    public class WidgetHub : Hub
    {
        public override Task OnConnected()
        {
            CancellationTokenSource src = new CancellationTokenSource();
            new DefaultWorker(new YahooFinDataSource(new List<string>() { "GOOG" }), 100)
                .Run(src.Token)
                .Subscribe(msg =>
                {
                    UpdateWidgetsData(JObject.Parse(msg));
                }, ex =>
                {
                    //Console.WriteLine(ex.Message);
                });

            return base.OnConnected();
        }

        public void UpdateWidgetsData(JObject msg)
        {
            Clients.All.updateWidgetsData(msg);
        }
    }
}