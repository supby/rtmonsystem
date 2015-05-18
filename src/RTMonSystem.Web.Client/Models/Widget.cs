using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTMonSystem.Web.Client.Models
{
    public enum WidgetViewType
    {
        None, JSOMPath
    }

    public class Widget
    {
        public Widget(): this(default(string), default(Type), default(string), default(string), 1000)
        {}

        public Widget(string name = default(string), Type sourceType = default(Type),
                        string title = default(string), string description = default(string), int refreshRate=1000)
        {
            Id = Guid.NewGuid();
            Name = name;
            SourceType = sourceType == default(Type) ? string.Empty : sourceType.Name;
            Title = title;
            Description = description;
            ViewType = WidgetViewType.None;
            RefreshRate = refreshRate;
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("SourceType")]
        public string SourceType { get; set; }

        [JsonProperty("ViewType")]
        public WidgetViewType ViewType { get; set; }

        [JsonProperty("RefreshRate")]
        public int RefreshRate { get; set; }
    }
}