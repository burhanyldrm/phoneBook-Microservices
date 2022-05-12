using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Report.Api.Data.Interfaces;
using Report.Api.Entities;

namespace Report.Api.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            ContactReports = database.GetCollection<ContactReport>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            ReportContextSeed.SeedData(ContactReports);
        }

        public IMongoCollection<ContactReport> ContactReports { get; }
    }
}
