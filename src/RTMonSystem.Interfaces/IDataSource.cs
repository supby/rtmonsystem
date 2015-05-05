﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.Interfaces
{
    public interface IDataSource<T>
    {
        Task<T> GetDataAsync();
    }
}
