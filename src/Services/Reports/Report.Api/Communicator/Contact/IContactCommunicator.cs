using Report.Api.Communicator.Contact.Model;
using System.Threading.Tasks;

namespace Report.Api.Communicator.Contact
{
    public interface IContactCommunicator
    {
        Task<GetReportModel> GetInfoByLocation(string location);
    }
}