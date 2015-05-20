using RTMonSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTMonSystem.Workers
{
    public class DefaultWorker<T> : IDataSourceWorker<T>
    {
        private readonly IDataSource<T> _ds;
        private readonly int _delay = 0;

        public DefaultWorker(IDataSource<T> ds, int delay=0)
        {
            _ds = ds;
            _delay = delay;
        }

        public IObservable<T> Run(CancellationToken ct)
        {
            Func<IObserver<T>, Task> s = obs =>
            {
                return Task.Run(() =>
                {
                    while (true)
                    {
                        _ds.GetDataAsync()
                           .ContinueWith(t =>
                           {
                               T data = t.Result;
                               obs.OnNext(data);
                           })
                            .Wait();
                        if (_delay > 0)
                            Task.Delay(_delay).Wait();

                        if (ct.IsCancellationRequested)
                            break;
                    }
                });
            };

            return Observable.Create<T>(s);
        }
    }
}
