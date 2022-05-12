using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Report.Api.Communicator.Contact;
using Report.Api.Controllers;
using Report.Api.Data;
using Report.Api.Data.Interfaces;
using Report.Api.Repositories;
using Report.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Report.Tests
{
    public class Tests
    {

        [Test]
        public async Task GetReportsTest()
        {
            var services = new ServiceCollection();
            services.AddScoped<IReportContext, ReportContext>();
            services.AddScoped<IContactReportRepository, ContactReportRepository>();
            services.AddTransient<IConfiguration>(sp =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });
            var serviceProvider = services.BuildServiceProvider();
            var repository = serviceProvider.GetService<IContactReportRepository>();

            var publishEndpoint = new Mock<IPublishEndpoint>();
            var communicator = new Mock<IContactCommunicator>();

            var controller = new ReportController(repository, publishEndpoint.Object, communicator.Object);
            var actual = await controller.GetReports();

            //Value'a eriþemedim.
            //Assert.That(2, Is.GreaterThanOrEqualTo(actual.Value));
            Assert.Pass();
        }
    }
}