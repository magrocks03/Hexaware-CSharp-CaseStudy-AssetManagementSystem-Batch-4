using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AssetManagementSystem.util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string filePath)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (line.Contains("="))
                    {
                        var parts = line.Split('=');
                        config[parts[0].Trim()] = parts[1].Trim();
                    }
                }

                return $"Server={config["Server"]};Database={config["Database"]};Trusted_Connection={config["Trusted_Connection"]};TrustServerCertificate={config["TrustServerCertificate"]};";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading db.properties: " + ex.Message);
                return null;
            }
        }
    }
}
