using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.Interfaces
{
    public interface IWorkersManager
    {
        void AttachWorker(string id, IDisposable worker);
        void DetachWorker(string id, IDisposable worker);
        void DetachWorkersById(string id);
    }
}
