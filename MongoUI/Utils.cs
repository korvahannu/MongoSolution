using Microsoft.Extensions.Configuration;

namespace MongoUI
{
    public class Utils
    {
        public static string GetConnectionString(string connectiongStringName = "Default") =>
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString(connectiongStringName);
    }
}
