﻿using System;
using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Services
{
    public class RegisterService : IRegisterService
    {
        public bool Register(users user)
        {
            using (var context = new vlEntities())
            {
                try
                {
                    context.users.Add(user);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
