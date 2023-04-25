using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Domain.Helpers
{
    public class ConfigHelper
    {
        private static IHostEnvironment _environment;

        public static void SetEnvironment(IHostEnvironment environment)
        {
            if (_environment == null)
            {
                _environment = environment;
            }
        }

        public static IHostEnvironment GetEnvironment()
        {
            return _environment;
        }

        private static IConfigurationRoot _appSettings;
        internal static IConfigurationRoot AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: true, reloadOnChange: true);
                    var environmentName = GetEnvironment()?.EnvironmentName;
                    if (environmentName != null)
                    {
                        builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
                    }
                    else
                    {
                        builder.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
                    }
                    _appSettings = builder.Build();
                }
                return _appSettings;
            }
        }

        public static string Get(string sessionName)
        {
            return AppSettings?.GetSection(sessionName)?.Value;
        }

        public static T Get<T>(string sessionName)
        {
            return AppSettings.GetSection(sessionName).Get<T>();
        }

        public static string GetChild(string sessionName, string key)
        {
            return AppSettings?.GetSection(sessionName)?.GetSection(key)?.Value;
        }

        public static T GetChild<T>(string sessionName, string key)
        {
            return AppSettings.GetSection(sessionName).GetSection(key).Get<T>();
        }
    }
}
