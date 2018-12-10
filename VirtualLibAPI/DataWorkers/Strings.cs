using System.Configuration;

namespace VirtualLibrarity.DataWorkers
{
    public static class Strings
    {
        public static string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString(); //const
        }
    }
}