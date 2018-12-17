using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualLibrarity.Models.Entities
{
    public class Book3
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Popularity { get; set; }
        public string Category { get; set; }
    }
}