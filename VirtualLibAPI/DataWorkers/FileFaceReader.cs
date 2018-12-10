using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using VirtualLibrarity.DataWorkers;

namespace VirtualLibAPI
{
    public class FileFaceReader:IReader
    {
        public int ReadInfo()
        {
            try
            {
                string buffer = File.ReadAllText(Strings.GetString("infoFilePath"));
                if (buffer == "")
                {
                    return 0;
                }
                else
                {
                    return int.Parse(buffer);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(Strings.GetString("ex3Message"));
                Console.Write(ex.Data);
                return -1;
            }
        }
        public List<string> ReadFaces(int numbOfFaces)
        {
            try
            {
                List<string> images64String = new List<string>();
                string fileName;
                string image64String;
                byte[] imageBytes;
                if (numbOfFaces == 0)
                    return null;
                else
                {
                    for (int i = 1; i <= numbOfFaces; i++)
                    {
                        fileName = Strings.GetString("facesFilePath") + i +
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