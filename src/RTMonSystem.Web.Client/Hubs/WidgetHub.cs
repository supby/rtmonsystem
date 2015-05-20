using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using RTMonSystem.DataSources;
using RTMonSystem.DataSources.REST.Yahoo;
using RTMonSystem.Interfaces;
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
        private readonly IWorkersManager _workersManager;

        public WidgetHub(IWorkersManager workersManager)
        {
            _workersManager = workersManager;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            StopWorkers();
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
                var w = new DefaultWorker<string>(new YahooFinDataSource(new List<string>() { "GOOG" }), widget.RefreshRate);
                w.Run()
                .Subscribe(msg =>
                {
                    UpdateWidgetsData(widget, JObject.Parse(msg));
                }, OnError);
                _workersManager.AttachWorker(Context.ConnectionId, w);
            }
            if (widget.SourceType == typeof(RandomNumberDataSource).Name)
            {
                var w = new DefaultWorker<int>(new RandomNumberDataSource(), widget.RefreshRate);
                w.Run()
                .Subscribe(val =>
                {
                    UpdateWidgetsData(widget, val);
                }, OnError);
                _workersManager.AttachWorker(Context.ConnectionId, w);
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

        private void StopWorkers()
        {
            _workersManager.DetachWorkersById(Context.ConnectionId);
        }
    }
}