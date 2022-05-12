using Report.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.Api.Repositories.Interfaces
{
    public interface IContactReportRepository
    {
        Task<IEnumerable<ContactReport>> GetReports();
        Task<ContactReport> GetReport(string id);
        Task CreateReport(ContactReport report);
        Task<bool> UpdateReport(ContactReport report);
    }
}
