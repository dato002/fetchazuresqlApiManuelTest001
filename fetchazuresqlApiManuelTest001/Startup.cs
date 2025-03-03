using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fetchazuresqlApiManuelTest001
{
    public class Startup : FunctionsStartup
    { 
        /*private static readonly IConfigurationRoot = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();*/


        public override void Configure(IFunctionsHostBuilder builder)
        {
            Contract.Requires(builder != null);
            builder.Services.AddHttpClient();

            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

            var configuration = configBuilder.Build();
            var conn = configuration["AzureSqlConnectionString"];

        }

    }
}
