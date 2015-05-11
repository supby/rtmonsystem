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

        public void ConnectRange(List<Widget> widgets)
        {
            foreach (Widget widget in widgets)
                Connect(widget);
        }

        public void Connect(Widget widget)
        {
            CancellationTokenSource src = new CancellationTokenSource();
            if (widget.Type == typeof(YahooFinDataSource).Name)
            {
                new DefaultWorker<string>(new YahooFinDataSource(new List<string>() { "GOOG" }), 1000)
                .Run(src.Token)
                .Subscribe(msg =>
                {
                    UpdateWidgetsData(widget, JObject.Parse(msg));
                }, OnError);
            }
            if (widget.Type == typeof(RandomNumberDataSource).Name)
            {
                new DefaultWorker<int>(new RandomNumberDataSource(), 1000)
                .Run(src.Token)
                .Subscribe(val =>
                {
                    UpdateWidgetsData(widget, val);
                }, OnError);
            }
        }

        public void UpdateWidgetsData(Widget widget, JObject msg)
        {
            Clients.Caller.updateWidgetsData(widget.Id, msg);
        }

        public void UpdateWidgetsData(Widget widget, int value)
        {
            Clients.Caller.updateWidgetsData(widget.Id, value);
        }
    }
}