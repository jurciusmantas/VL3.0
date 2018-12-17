using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using VirtualLibrarity.DataWorkers;

namespace VirtualLibAPI
{
    public class FileFaceReader:IReader
    {
        public List<int> ReadInfo()
        {
                string buffer = File.ReadAllText(Strings.GetString("infoFilePath"));
                if (buffer == "")
                {
                return null;
                }
                else
                {
                    string[] idstr = buffer.Split(',');
                    int[] ids = Array.ConvertAll(idstr,int.Parse);
                return new List<int>(ids);
                }
        }
        public List<string> ReadFaces(List<int> idFaces)
        {
            try
            {
                List<string> images64String = new List<string>();
                string fileName;
                string image64String;
                byte[] imageBytes;
                if (idFaces.Count == 0)
                    return null;
                else
                {
                    foreach(int id in idFaces)
                    {
                        fileName = Strings.GetString("facesFilePath") + id +
                            Strings.GetString("facesFileType");
                        imageBytes = File.ReadAllBytes(fileName);
                        image64String = Convert.ToBase64String(imageBytes);
                        images64String.Add(image64String);
                    }
                    return images64String;
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine(Strings.GetString("ex4Message"));
                Console.Write(ex.Data);
                return null;
            }
        }
    }
}