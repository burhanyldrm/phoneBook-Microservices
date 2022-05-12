using MongoDB.Driver;
using Report.Api.Entities;

namespace Report.Api.Data.Interfaces
{
    public interface IReportContext
    {
        IMongoCollection<ContactReport> ContactReports { get; }
    }
}
