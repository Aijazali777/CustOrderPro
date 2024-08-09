using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CustOrderPro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Below instruction calls the BuildWebHost method, and then starts the web application to handle incoming web requests.
            BuildWebHost(args).Run();
        }

        // BuildWebHost method configures and builds the web host
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
