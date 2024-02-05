using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarRentalSystem.Utility
{
    internal static class DBConnUtil
    {
        private static IConfiguration _iconfiguration;

        static DBConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
        }

        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("LocalConnString");
        }
    }
}
