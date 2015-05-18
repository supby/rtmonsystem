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
        private CancellationTokenSource _ctSrc;

        public WidgetHub()
        {
            _ctSrc = new CancellationTokenSource();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _ctSrc.Cancel();
            return base.OnDisconnected(stopCalled);
        }

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
            if (widget.SourceType == typeof(YahooFinDataSource).Name)
            {
                new DefaultWorker<string>(new YahooFinDataSource(new List<string>() { "GOOG" }), widget.RefreshRate)
                .Run(_ctSrc.Token)
                .Subscribe(msg =>
                {
                    UpdateWidgetsData(widget, JObject.Parse(msg));
                }, OnError);
            }
            if (widget.SourceType == typeof(RandomNumberDataSource).Name)
            {
                new DefaultWorker<int>(new RandomNumberDataSource(), widget.RefreshRate)
                .Run(_ctSrc.Token)
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