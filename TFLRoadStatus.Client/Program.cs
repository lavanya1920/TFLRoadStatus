using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using TFLRoadStatus.API.Service;
using TFLRoadStatus.API.Interfaces;
using TFLRoadStatus.API.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace TFLRoadStatus.Client
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var roadId = args[0];
                var serviceProvider = ConfigureServices();
                var roadStatusService = serviceProvider.GetService<IRoadStatusService>();
                try
                {
                    var result = roadStatusService.GetRoadStatus(roadId).GetAwaiter().GetResult();
                    if (result.IsSuccess)
                    {

                        Console.WriteLine($"The status of the {result.DisplayName} is as follows:");
                        Console.WriteLine($"Road Status is {result.StatusSeverity}");
                        Console.WriteLine($"Road Status Description is {result.StatusSeverityDescription}");
                    }
                    else
                    {
                        Console.WriteLine($"{roadId} is not a valid road");
                        Environment.Exit(1);
                    }

                }
                catch (Exception ex)
                {
                    //log Exception
                    Console.WriteLine($"There was an error running the application : "+ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Please run as TFLRoadStatus.Client.exe RoadId.... eg : TFLRoadStatus.Client.exe A2");
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IRoadStatusService, RoadStatusService>();
            services.AddSingleton(Configuration.GetSection("TFLAPIDetails").Get<TFLAPIDetails>());

            return services.BuildServiceProvider();
        }
    }
}