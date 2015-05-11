using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTMonSystem.Web.Client.Models
{
    public class JSONWidget : Widget
    {
        public JSONWidget(string name = default(string), Type type = default(Type), 
                    string title = default(string), string description = default(string), string path = default(string), 
                    int refreshRate = 1000)
            : base(name, type, title, description)
        {
            Path = path;
            ViewType = WidgetViewType.JSOMPath;
        }

        [JsonProperty("Path")]
        public string Path { get; set; }
    }
}