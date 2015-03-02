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
    class YahooFinWorker : IDataSourceWorker
    {
        private readonly IDataSource _ds;

        public YahooFinWorker(IDataSource ds)
        {
            _ds = ds;
        }

        public IObservable<string> Subscribe(CancellationToken ct)
        {
            Func<IObserver<string>, Task> s = async obs =>
                            //Task.Factory.StartNew(() =>
                            {
                                while (true)
                                {
                                    string data = await _ds.GetDataAsync(new Dictionary<string, string>() {
                                        {"symbols", "GOOG"}
                                    });
                                    if (string.IsNullOrEmpty(data))
                                        obs.OnNext(data);
                                    ct.ThrowIfCancellationRequested();
                                }
                            };
                            //}, ct);

            return Observable.Create<string>(s);
        }
    }
}
