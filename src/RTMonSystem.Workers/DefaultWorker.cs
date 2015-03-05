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
    public class DefaultWorker : IDataSourceWorker
    {
        private readonly IDataSource _ds;
        private readonly int _delay = 0;

        public DefaultWorker(IDataSource ds, int delay=0)
        {
            _ds = ds;
            _delay = delay;
        }

        public IObservable<string> Run(CancellationToken ct)
        {
            Func<IObserver<string>, Task> s = async obs =>
                            {
                                while (true)
                                {
                                    string data = await _ds.GetDataAsync();
                                    if (!string.IsNullOrEmpty(data))
                                        obs.OnNext(data);
                                    ct.ThrowIfCancellationRequested();
                                    if(_delay > 0)
                                        await Task.Delay(_delay, ct);
                                }
                            };

            return Observable.Create<string>(s);
        }
    }
}
