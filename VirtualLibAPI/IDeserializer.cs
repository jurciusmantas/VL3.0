using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    interface IDeserializer
    {
        Image<TColor, TDepth> CreateImage<TColor, TDepth>(string image64String)
           where TColor : struct, IColor
           where TDepth : new();





    }
}
