using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualLibrarity.EFModel;

namespace VirtualLibrarity.Services
{
    public class DeleteService : IDeleteService
    {
        public bool DeleteUser(int UserId)
        {
            using (var context = new vlEntities())
            {
                try
                {
                    context.users.Remove(context.users.Where(u => u.Id == UserId).FirstOrDefault());
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