using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Exceptions;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactWithInfo
{
    public class GetContactWithInfoQueryHandler : IRequestHandler<GetContactWithInfoQuery, ContactPersonWithInfoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactWithInfoQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ContactPersonWithInfoResponse> Handle(GetContactWithInfoQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<ContactPerson>().FindByProperties(x => x.Id == request.ContactId,
                "ContactPersonInfos");

            if(entity == null)
                throw new NotFoundException(nameof(ContactPerson), request.ContactId);

            var response = new ContactPersonWithInfoResponse() 
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Firm = entity.Firm,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate
            };

            response.ContactInfos = _mapper.Map<List<ContactInfoResponse>>(entity.ContactPersonInfos);

            return response;
        }
    }
}
