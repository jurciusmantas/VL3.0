using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualLibrarity.Database;

namespace VirtualLibrarity.DataWorkers
{
    public class BookRecognitionHandler
    {
        public bool Return(string QRCode, int userid)
        {
            int qrcode = int.Parse(QRCode);
            var updateCopies = new MySqlCommand(@"
                UPDATE Copies
                SET UserId = NULL 
                WHERE Id = @id AND
                UserId = @userid");
            updateCopies.Parameters.AddWithValue("@id", qrcode);
            updateCopies.Parameters.AddWithValue("@userid", userid);



           return  DatabaseConnector.UpdateData(updateCopies);  
        }
        public bool Take(string QRCode, int userid)
        {
            int qrcode = int.Parse(QRCode);
            var updateCopies = new MySqlCommand(@"
                UPDATE Copies
                SET UserId = @userid
                WHERE Id = @id");
            updateCopies.Parameters.AddWithValue("@userid", userid);
            updateCopies.Parameters.AddWithValue("@id", qrcode);
            return DatabaseConnector.UpdateData(updateCopies);   //čia reikia return bookinfo;
        }
    }
}