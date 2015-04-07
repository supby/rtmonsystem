using RTMonSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RTMonSystem.DataSources.REST
{
    public class RESTDataSource : IDataSource
    {
        private readonly Uri _uri;
        public RESTDataSource(string url) : this(new Uri(url))
        { }

        public RESTDataSource(Uri uri)
        {
            _uri = uri;
        }

        protected RESTDataSource()
        { }

        public async virtual Task<string> GetDataAsync()
        {
            //string url = string.Format("https://query.yahooapis.com/v1/public/yql?q={0}&format=json&diagnostics=false",
            //                            Uri.EscapeDataString(
            //                                    string.Format("select * from yahoo.finance.quotes where symbol in ({0})",
            //                                                p["symbols"])));
            return await GetDataAsync(_uri);
        }

        protected async Task<string> GetDataAsync(Uri uri)
        {
            WebRequest request = WebRequest.Create(uri);
            string resp = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var respStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(respStream))
                    {
                        resp = await reader.ReadToEndAsync();
                    }
                }
            }
            return resp;
        }
    }
}
