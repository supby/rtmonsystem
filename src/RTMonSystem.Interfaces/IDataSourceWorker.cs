using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTMonSystem.Interfaces
{
    public interface IDataSourceWorker
    {
        IObservable<string> Subscribe(CancellationToken ct);
    }
}
