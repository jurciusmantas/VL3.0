using System;
using System.Collections.Generic;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Models
{
    public struct RegisterArgs
    {
        public User User { get; set; }
        public string Image { get; set; }
    }
}