namespace VirtualLibDatabase.Services
{
    public class RegisterService
    {
        public RegisterService()
        {

        }
        public void Register(users user)
        {
            using (var context = new vlEntities())
            {
                context.users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
