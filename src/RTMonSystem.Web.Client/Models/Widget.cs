using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTMonSystem.Web.Client.Models
{
    public class Widget
    {
        public Widget()
        { }
        public Widget(string name, Type type, string title, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type.Name;
            Title = title;
            Description = description;
        }
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}