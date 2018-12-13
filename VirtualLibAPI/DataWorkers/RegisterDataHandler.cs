using MySql.Data.MySqlClient;
using VirtualLibrarity.Database;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.DataWorkers
{
    public class RegisterDataHandler : IRegisterDataHandler
    {
        public bool Insert(User user, int id)
        {
            var insert = new MySqlCommand(@"
                INSERT IGNORE INTO users
                VALUES(@id, @firstName, @lastName, @email, @password)");
            insert.Parameters.AddWithValue("@id", id);
            insert.Parameters.AddWithValue("@firstName", user.Firstname);
            insert.Parameters.AddWithValue("@lastName", user.Lastname);
            insert.Parameters.AddWithValue("@email", user.Email);
            insert.Parameters.AddWithValue("@password", user.Password);

            var res = DatabaseConnector.GetData(insert);

            return res.Success;
        }
    }
}