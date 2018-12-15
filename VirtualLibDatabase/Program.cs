using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new vlEntities())
            {
                Console.WriteLine(context.users.Where(u => u.Email == "j")
                    .FirstOrDefault().Id);
                Console.ReadKey();
            }
        }
    }
}
