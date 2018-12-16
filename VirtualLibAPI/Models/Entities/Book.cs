using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualLibrarity.EFModel;

namespace VirtualLibrarity.Models.Entities
{
    public class Book
    {
        public books BookInfo { get; set; }
        public int Amount { get; set; }
    }
}