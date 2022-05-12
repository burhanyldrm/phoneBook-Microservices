using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Exceptions;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersonInfos.Commands.SaveContactPersonInfo
{
    public class SaveContactPersonInfoHandler : IRequestHandler<SaveContactPersonInfoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SaveContactPersonInfoHandler> _logger;

        public SaveContactPersonInfoHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SaveContactPersonInfoHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SaveContactPersonInfoCommand request, CancellationToken cancellationToken)
        {
            var contactPerson = await _unitOfWork.Repository<ContactPerson>().GetById(request.ContactPersonId);

            if(contactPerson == null)
                throw new NotFoundException(nameof(ContactPerson), request.ContactPersonId);

            var contactPersonInfo = _mapper.Map<ContactPersonInfo>(request);

            contactPerson.VerifyOrAddContactInfo(contactPersonInfo);

            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
