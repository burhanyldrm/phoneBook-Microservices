using MongoDB.Driver;
using Report.Api.Data.Interfaces;
using Report.Api.Entities;
using Report.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.Api.Repositories
{
    public class ContactReportRepository : IContactReportRepository
    {
        private readonly IReportContext _context;

        public ContactReportRepository(IReportContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ContactReport>> GetReports()
        {
            return await _context
                .ContactReports.Find(p => true).ToListAsync();
        }

        public async Task<ContactReport> GetReport(string id)
        {
            return await _context
                .ContactReports.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateReport(ContactReport report)
        {
            await _context.ContactReports.InsertOneAsync(report);
        }

        public async Task<bool> UpdateReport(ContactReport report)
        {
            var updateResult = await _context
                .ContactReports.ReplaceOneAsync(filter: g => g.Id == report.Id, replacement: report);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
