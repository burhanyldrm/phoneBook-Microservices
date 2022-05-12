using MediatR;

namespace Contact.Application.Features.ContactPersons.Queries.GetReportWithLocation
{
    public class GetReportWithLocationQuery : IRequest<GetReportWithLocationResponse>
    {
        public string Location { get; set; }

        public GetReportWithLocationQuery(string location)
        {
            Location = location;
        }
    }
}
