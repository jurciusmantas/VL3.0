using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibAPI
{
    public interface IWriter
    {
        bool WriteFaceToFile(int id, string image64String);
        bool WriteInfoFile(int number);
    }
}
