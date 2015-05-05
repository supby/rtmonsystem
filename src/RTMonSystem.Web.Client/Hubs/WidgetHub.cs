using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using RTMonSystem.DataSources;
using RTMonSystem.DataSources.REST.Yahoo;
using RTMonSystem.Web.Client.Models;
using RTMonSystem.Workers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RTMonSystem.Web.Client.Hubs
{
    public class WidgetHub : Hub
    {
        private void OnError(Exception ex)
        {
            //Console.WriteLine(ex.Message);
        }

        public void Connect(string widgetType)
        {
            CancellationTokenSource src = new CancellationTokenSource();
            if (widgetType == typeof(YahooFinDataSource).Name)
            {
                new DefaultWorker<string>(new YahooFinDataSource(new List<string>() { "GOOG" }), 100)
                .Run(src.Token)
                .Subscribe(msg =>
                {
                    UpdateWidgetsData(JObject.Parse(msg));
                }, OnError);
            }
            if (widgetType == typeof(RandomNumberDataSource).Name)
            {
                new DefaultWorker<int>(new RandomNumberDataSource(), 100)
                .Run(src.Token)
                .Subscribe(val =>
                {
                    UpdateWidgetsData(val);
                }, OnError);
            }
        }

        public void UpdateWidgetsData(JObject msg)
        {
            Clients.Caller.updateWidgetsData(msg);
        }

        public void UpdateWidgetsData(int value)
        {
            Clients.Caller.updateWidgetsData(value);
        }
    }
}