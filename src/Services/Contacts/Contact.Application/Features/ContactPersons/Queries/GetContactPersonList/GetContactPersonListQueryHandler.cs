using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactPersonList
{
    public class GetContactPersonListQueryHandler : IRequestHandler<GetContactPersonListQuery, List<ContactPersonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactPersonListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ContactPersonResponse>> Handle(GetContactPersonListQuery request, CancellationToken cancellationToken)
        {
            var contactPersonList = await _unitOfWork.Repository<ContactPerson>().GetAll();
            return _mapper.Map<List<ContactPersonResponse>>(contactPersonList);
        }
    }
}
