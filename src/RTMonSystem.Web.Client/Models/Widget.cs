using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTMonSystem.Web.Client.Models
{
    public class Widget
    {
        public Widget(string name, string type, string title, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Title = title;
            Description = description;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}