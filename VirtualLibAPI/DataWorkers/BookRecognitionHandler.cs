using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualLibDatabase;
using VirtualLibDatabase.Entities;
using VirtualLibrarity.Database;
using VirtualLibrarity.Models.Entities;
using VirtualLibrarityDatabase.Entities;

namespace VirtualLibrarity.DataWorkers
{
    public class BookRecognitionHandler
    {
        public BookResponse Return(string QRCode, int userid)
        {
            int qrcode = int.Parse(QRCode);
            var updateCopies = new MySqlCommand(@"
                UPDATE Copies
                SET UserId = NULL 
                WHERE Id = @id AND
                UserId = @userid");
            updateCopies.Parameters.AddWithValue("@id", qrcode);
            updateCopies.Parameters.AddWithValue("@userid", userid);
            return new BookResponse
            {
                WasUpdated = DatabaseConnector.UpdateData(updateCopies)
            }; 
        }
        public BookResponse Take(string QRCode, int userid)
        {
            int qrcode = int.Parse(QRCode);
            var updateCopies = new MySqlCommand(@"
                UPDATE Copies
                SET UserId = @userid
                WHERE Id = @id AND UserId is null");
            updateCopies.Parameters.AddWithValue("@userid", userid);
            updateCopies.Parameters.AddWithValue("@id", qrcode);
            var selectBook = new MySqlCommand(@"
                SELECT Copies.Id, Books.Author, Books.Title
                FROM Copies,Books
                WHERE Copies.Id = @id");
            selectBook.Parameters.AddWithValue("@id", qrcode);
            DbResponse response = DatabaseConnector.GetData(selectBook);
            Book bookInfo = new Book
            {
                QRCode = int.Parse(response.Data.Rows[0]["Id"].ToString()),
                Author = response.Data.Rows[0]["Author"].ToString(),
                Title = response.Data.Rows[0]["Title"].ToString()
            };
            return new BookResponse
            {
                BookInfo = bookInfo,
                WasUpdated = DatabaseConnector.UpdateData(updateCopies)
            };   
        }
    }
}