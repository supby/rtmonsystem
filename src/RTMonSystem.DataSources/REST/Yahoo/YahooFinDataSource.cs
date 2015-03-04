using RTMonSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.DataSources.REST.Yahoo
{
    public sealed class YahooFinDataSource : RESTDataSource
    {
        private const string YAHOO_FIN_FORMAT_URL = "";
        private readonly List<string> _symbols;

        public YahooFinDataSource(List<string> symbols)
        {
            _symbols = symbols;
        }

        public override async Task<string> GetDataAsync()
        {
            string url = string.Format("https://query.yahooapis.com/v1/public/yql?q={0}&format=json&diagnostics=false&env=http%3A%2F%2Fdatatables.org%2Falltables.env",
                                        Uri.EscapeDataString(string.Format("select * from yahoo.finance.quotes where symbol in (\"{0}\")",
                                                _symbols.Aggregate((s1,s2) => string.Format("{0}\",\"{1}", s1, s2)))));
            return await GetDataAsync(url);
        }
    }
}
