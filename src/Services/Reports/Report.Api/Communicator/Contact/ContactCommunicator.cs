using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Report.Api.Communicator.Contact.Model;
using Report.Api.Communicator.Helper;
using System.Threading.Tasks;

namespace Report.Api.Communicator.Contact
{
    public class ContactCommunicator : IContactCommunicator
    {
        private static string _baseUrl;

        public ContactCommunicator(IConfiguration configuration)
        {
            _baseUrl = configuration["ContactBaseUrl"];
        }

        public async Task<GetReportModel> GetInfoByLocation(string location)
        {
            IRestClientHelper _restClientHelper = new RestClientHelper();
            var result = await _restClientHelper.GetAsync($"{_baseUrl}/GetReportWithLocation?location={location}");

            return JsonConvert.DeserializeObject<GetReportModel>(result);
        }
    }
}
