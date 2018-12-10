using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    public interface IRecognizer
    {
       int Recognize(List<string> faces, string faceFromApp);
    }
}
