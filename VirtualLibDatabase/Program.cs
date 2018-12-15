using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibDatabase.Services;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterService service = new RegisterService();
            service.Register(new User
            {
                Firstname = "test",
                Lastname = "test",
                Email = "test@test.com",
                Password = "test"
            });
        }
    }
}
