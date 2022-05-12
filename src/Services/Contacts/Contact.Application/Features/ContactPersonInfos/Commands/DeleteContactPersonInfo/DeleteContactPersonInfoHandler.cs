using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Exceptions;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersonInfos.Commands.DeleteContactPersonInfo
{
    public class DeleteContactPersonInfoHandler : IRequestHandler<DeleteContactPersonInfoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteContactPersonInfoHandler> _logger;

        public DeleteContactPersonInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            ILogger<DeleteContactPersonInfoHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteContactPersonInfoCommand request, CancellationToken cancellationToken)
        {
            var contactPerson = await _unitOfWork.Repository<ContactPerson>().Find(x => x.Name == request.Name
            && x.Surname == request.Surname);

            if (contactPerson == null)
                throw new NotFoundException(nameof(ContactPerson), $"{request.Name} {request.Surname}");

            contactPerson.DeleteContactInfo(request.ContactInfoType);

            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
