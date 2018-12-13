using MySql.Data.MySqlClient;
using VirtualLibrarity.Models;
using VirtualLibrarity.Models.Entities;
using VirtualLibrarity.Database;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace VirtualLibrarity.DataWorkers
{
    public class UserToLoginResponseBuilder : IUserToLoginResponseBuilder
    {
        public UserToLoginResponse BuildUserToSend(int id)
        {
            UserToLoginResponse user = new UserToLoginResponse();
            if (id < 0)
            {
                user.ExceptionMessage = "Internal server exception has happened";
            }
            else if (id == 0)
            {
                user.ExceptionMessage = "User not found";
            }
            else
            {
                var selectUser = new MySqlCommand(@"
                    SELECT u.Id, u.FirstName, u.LastName, u.Email
                    FROM users AS u
                    WHERE u.Id = @id");
                selectUser.Parameters.AddWithValue("@id", id);

                var selectUserRes = DatabaseConnector.GetData(selectUser);

                if (selectUserRes.Success)
                {
                    user.UserInfo = new User()
                    {
                        Id = int.Parse(selectUserRes.Data.Rows[0]["Id"].ToString()),
                        Firstname = selectUserRes.Data.Rows[0]["FirstName"].ToString(),
                        Lastname = selectUserRes.Data.Rows[0]["LastName"].ToString(),
                        Email = selectUserRes.Data.Rows[0]["Email"].ToString(),
                    };
                    user.BorrowedBooks = GetBooks(id);
                }
                else
                {
                    user.ExceptionMessage = "Face recognition login : user not found";
                }
            }
            return user;
        }
        public UserToLoginResponse BuildUserToSend(LoginManualArgs loginManualArgs)
        {
            var selectUser = new MySqlCommand(@"
                SELECT u.Id, u.FirstName, u.LastName FROM users AS u
                WHERE u.Email = @email AND
                u.Password = @password ");
            selectUser.Parameters.AddWithValue("@email", loginManualArgs.Email);
            selectUser.Parameters.AddWithValue("@password", loginManualArgs.Password);

            var selectUserRes = DatabaseConnector.GetData(selectUser);

            if (selectUserRes.Success)
            {
                return new UserToLoginResponse()
                {
                    UserInfo = new User
                    {
                        Id = int.Parse(selectUserRes.Data.Rows[0]["Id"].ToString()),
                        Firstname = selectUserRes.Data.Rows[0]["FirstName"].ToString(),
                        Lastname = selectUserRes.Data.Rows[0]["LastName"].ToString(),
                        Email = loginManualArgs.Email
                    },
                    BorrowedBooks = GetBooks((int)selectUserRes.Data.Rows[0]["Id"])
                };
            }
            return new UserToLoginResponse() { ExceptionMessage = "Manual login : user not found"};
        }

        private List<Book> GetBooks(int userId)
        {
            var selectBooks = new MySqlCommand(@"
                    SELECT b.Title, b.Author, c.Id FROM books AS b
                    INNER JOIN copies AS c ON c.BookId = b.Id
                    INNER JOIN users AS u ON u.Id = c.UserId
                    WHERE u.Id = @userId");
            selectBooks.Parameters.AddWithValue("@userId", userId);


           

            var selectBooksRes = DatabaseConnector.GetData(selectBooks);

            if (selectBooksRes.Success)
                return selectBooksRes.Data.AsEnumerable().Select(
                    row => new Book() { Author = row.Field<string>("Author"), Title = row.Field<string>("Title"),QRCode = row.Field<int>("Id")}).ToList();
            else return null;
        }
    }
}