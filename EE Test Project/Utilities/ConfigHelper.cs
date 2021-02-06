using Microsoft.Extensions.Configuration;

namespace EE_Test_Project.Utilities
{
    public class ConfigHelper
    {
        private static string configFile = "appSettings.QA.json";

        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile(configFile, optional: false, reloadOnChange: true);

            return builder.Build();
        }

    }
}
