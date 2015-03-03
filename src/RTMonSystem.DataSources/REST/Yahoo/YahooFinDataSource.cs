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
            string url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22YHOO%22%2C%22AAPL%22%2C%22GOOG%22%2C%22MSFT%22)%0A%09%09&format=json&diagnostics=true&env=http%3A%2F%2Fdatatables.org%2Falltables.env&callback=";
            return await GetDataAsync(url);
        }
    }
}
