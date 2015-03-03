using Newtonsoft.Json.Linq;
using RTMonSystem.DataSources;
using RTMonSystem.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTMonSystem.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource src = new CancellationTokenSource();
            new DefaultWorker(new YahooFinDataSource(new List<string>()), 1000)
                .Run(src.Token).Subscribe(msg =>
                {
                    JObject obj = JObject.Parse(msg);
                    Console.WriteLine(obj["query"]["results"]["quote"][0]["AskRealtime"].ToString());
                });

            Console.ReadLine();
        }
    }
}
