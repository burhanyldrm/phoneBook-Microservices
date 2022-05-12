using Contact.Application.Contracts.Persistence;
using Contact.Domain.ContactPersonAggregate;
using Contact.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersons.Queries.GetReportWithLocation
{
    public class GetReportWithLocationQueryHandler : IRequestHandler<GetReportWithLocationQuery, GetReportWithLocationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReportWithLocationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<GetReportWithLocationResponse> Handle(GetReportWithLocationQuery request, CancellationToken cancellationToken)
        {
            var response = new GetReportWithLocationResponse { Location = request.Location, ContactCount = 0, ContactPhoneCount = 0 };

            var contactInfos = await _unitOfWork.Repository<ContactPersonInfo>().Filter(x => x.Type == ContactInfoType.Location 
            && x.Info == request.Location);

            if (contactInfos.Count == 0)
                return response;

            var contactPersonIds = contactInfos.Select(x => x.ContactPersonId).Distinct();

            response.ContactCount = contactPersonIds.Count();

            response.ContactPhoneCount = await _unitOfWork.Repository<ContactPersonInfo>().CountExpression(x => contactPersonIds.Contains(x.ContactPersonId)
            && x.Type == ContactInfoType.Phone);

            return response;

        }
    }
}
