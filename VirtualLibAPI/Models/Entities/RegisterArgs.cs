using System;
using System.Collections.Generic;
using VirtualLibDatabase;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Models
{
    public struct RegisterArgs
    {
        public users User { get; set; }
        public string Image { get; set; }
    }
}