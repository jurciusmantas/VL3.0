using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    public interface IReader
    {
        int ReadInfo();
        List<string> ReadFaces(int numbOfFaces);
    }
}
