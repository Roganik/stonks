using System;
using Microsoft.Extensions.Configuration;

namespace Stonks.Cfg
{
    public class ConfigBuilder
    {
        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "STONKS_");

            return builder.Build();
        }
    }
}