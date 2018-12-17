using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    public interface IReader
    {
        List<int> ReadInfo();
        List<string> ReadFaces(List<int> facesid);
    }
}
