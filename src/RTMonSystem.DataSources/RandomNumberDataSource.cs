using RTMonSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.DataSources
{
    public class RandomNumberDataSource : IDataSource<int>
    {
        private readonly Random _rnd;
        public RandomNumberDataSource()
        {
            _rnd = new Random(42);
        }
        public Task<int> GetDataAsync()
        {
            return Task.FromResult<int>(_rnd.Next());
        }
    }
}
