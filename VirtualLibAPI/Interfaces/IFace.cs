using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrarity.Interfaces
{
    public interface IFace
    {
        string Image64String { get; set; }
        bool IsForSave { get; set; }
    }
}
