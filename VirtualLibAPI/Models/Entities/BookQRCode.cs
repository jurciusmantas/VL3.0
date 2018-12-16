using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualLibrarity.Models.Entities
{
    public class BookQRCode
    {
        public bool IsTaking { get; set; }
        public string QRCode { get; set; }
        public int UserId { get; set; }
    }
}