using MongoDB.Driver;
using Report.Api.Entities;
using Report.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Report.Api.Data
{
    public class ReportContextSeed
    {
        public static void SeedData(IMongoCollection<ContactReport> contactReportCollection)
        {
            bool existReport = contactReportCollection.Find(p => true).Any();
            if (!existReport)
            {
                contactReportCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<ContactReport> GetPreconfiguredProducts()
        {
            return new List<ContactReport> {
                new ContactReport()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    CreatedDate = DateTime.Now,
                    Status = State.Done
                },
                new ContactReport()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    CreatedDate = DateTime.Now,
                    Status = State.Pending
                },
            };
        }
    }
}
