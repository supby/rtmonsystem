using RTMonSystem.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.Workers
{
    public class WorkersManager : IDisposable, IWorkersManager
    {
        private readonly ConcurrentDictionary<string, List<IDisposable>> _workersMap;
        public WorkersManager()
        {
            _workersMap = new ConcurrentDictionary<string, List<IDisposable>>();
        }

        public void Dispose()
        {
            foreach (var k in _workersMap.Keys)
                foreach (var v in _workersMap[k])
                    v.Dispose();
        }

        public void AttachWorker(string id, IDisposable worker)
        {
            if (!_workersMap.ContainsKey(id))
                _workersMap[id] = new List<IDisposable>();
            _workersMap[id].Add(worker);
        }

        public void DetachWorker(string id, IDisposable worker)
        {
            if (!_workersMap.ContainsKey(id))
                return;

            _workersMap[id].Find(w => w == worker).Dispose();
            _workersMap[id].Remove(worker);
        }


        public void DetachWorkersById(string id)
        {
            if (!_workersMap.ContainsKey(id))
                return;
            
            List<IDisposable> toRemove;
            _workersMap.TryRemove(id, out toRemove);
            if(toRemove != null)
                toRemove.ForEach(w => w.Dispose());
        }
    }
}
