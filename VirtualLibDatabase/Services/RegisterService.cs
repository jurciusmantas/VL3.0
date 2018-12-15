using System;

namespace VirtualLibDatabase.Services
{
    public class RegisterService
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
