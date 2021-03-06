﻿using System.Linq;
using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Services
{
    public class LoginService : ILoginService
    {
        public users ManualLogin(string email, string password)
        {
            using(var context = new vlEntities())
            {
                return context.users
                    .Where(u => u.Email == email)
                    .Where(u => u.Password == password)
                    .FirstOrDefault();
            }
        }

        public users FaceRecognitionLogin(int id)
        {
            using(var context = new vlEntities())
            {
                return context.users
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
            }
        }
    }
}
