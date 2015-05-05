using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTMonSystem.Web.Client.Models
{
    public class Widget
    {
        public Widget(string name, Type type, string title, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type.Name;
            Title = title;
            Description = description;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
    }
}