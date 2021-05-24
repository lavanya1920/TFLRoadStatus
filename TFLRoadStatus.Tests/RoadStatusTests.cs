using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TFLRoadStatus.API.Interfaces;
using TFLRoadStatus.API.Models;
using TFLRoadStatus.API.Service;

namespace TFLRoadStatus.Test
{
    public class RoadStatusTest
    {
        private RoadStatus validRoadStatus;
        private RoadStatus InValidRoadStatus;
        public static IConfigurationRoot Configuration { get; set; }
        public IRoadStatusService service;
        [SetUp]
        public void Setup()
        {
            var serviceProvider = ConfigureServices();
            service = serviceProvider.GetService<IRoadStatusService>();
            validRoadStatus = new RoadStatus
            {
                IsSuccess=true,
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No Exceptional Delays"
            };
            InValidRoadStatus = new RoadStatus
            {
                IsSuccess = false,
                StatusSeverityDescription = "Not Found"
            };
        }

        [Test]
        public void Check_ValidRoadStatus()
        {
            var result = service.GetRoadStatus("A2").GetAwaiter().GetResult();
            Assert.AreEqual(validRoadStatus.IsSuccess, result.IsSuccess);
            Assert.AreEqual(validRoadStatus.DisplayName, result.DisplayName);
            Assert.AreEqual(validRoadStatus.StatusSeverity, result.StatusSeverity);
            Assert.AreEqual(validRoadStatus.StatusSeverityDescription, result.StatusSeverityDescription);
        }

        [Test]
        public void Check_InValidRoadStatus()
        {
            var result = service.GetRoadStatus("A233").GetAwaiter().GetResult();
            Assert.AreEqual(InValidRoadStatus.IsSuccess, result.IsSuccess);
            Assert.AreEqual(InValidRoadStatus.StatusSeverityDescription, result.StatusSeverityDescription);
        }

        private static ServiceProvider ConfigureServices()
        {
            // Build config
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.test.json", true, true)
                .AddEnvironmentVariables().Build();

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IRoadStatusService, RoadStatusService>();
            services.AddSingleton(config.GetSection("TFLAPIDetails").Get<TFLAPIDetails>());

            return services.BuildServiceProvider();
        }
    }
}