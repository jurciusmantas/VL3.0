using System;
using System.Configuration;
using System.IO;
using VirtualLibrarity.DataWorkers;

namespace VirtualLibAPI
{
    public class FileFaceWriter:IWriter
    {
       
        public bool WriteFaceToFile(int id, string image64String)
        {
                byte[] imageBytes = Convert.FromBase64String(image64String);
                File.WriteAllBytes(Strings.GetString("facesFilePath") + id 
                    + Strings.GetString("facesFileType"), imageBytes);
                return true;
        }
        public bool WriteInfoFile(int number)
        {
            try
            {
                File.AppendAllText(Strings.GetString("infoFilePath"),","+number.ToString());
                return true;
            }
            catch(IOException ex)
            {
                Console.WriteLine(Strings.GetString("ex2Message"));
                Console.Write(ex.StackTrace);
                return false;
            }
        }

        
    }
}