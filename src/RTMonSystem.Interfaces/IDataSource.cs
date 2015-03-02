using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.Interfaces
{
    public interface IDataSource
    {
        Task<string> GetDataAsync(Dictionary<string,string> p);
    }
}
