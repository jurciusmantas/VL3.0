using System;
using System.Collections.Generic;
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
                if(number==1)
                    File.AppendAllText(Strings.GetString("infoFilePath"),number.ToString());
                else
                    File.AppendAllText(Strings.GetString("infoFilePath"),','+ number.ToString());
                return true;
            }
            catch(IOException ex)
            {
                Console.WriteLine(Strings.GetString("ex2Message"));
                Console.Write(ex.StackTrace);
                return false;
            }
        }
        public bool WriteAllIdsIntoInfoFile(List<int> ids)
        {
            var Ids = new List<string>();
            for(int i=0; i<ids.Count; i++)
            {
                if (i == 0)
                {
                    Ids.Add(ids[i].ToString());
                }
                else
                    Ids.Add(','+ids[i].ToString());
            }
            try
            {
                File.WriteAllText(Strings.GetString("infoFilePath"), "");
                foreach (string str in Ids) { 
                File.AppendAllText(Strings.GetString("infoFilePath"), str);
                }
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(Strings.GetString("ex2Message"));
                Console.Write(ex.StackTrace);
                return false;
            }
        }
        public bool DeleteFile(int name)
        {
            try
            {
                File.Delete(Strings.GetString("facesFilePath") + name.ToString() + Strings.GetString("facesFileType"));
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