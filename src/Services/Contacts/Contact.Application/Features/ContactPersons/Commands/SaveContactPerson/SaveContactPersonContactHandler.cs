using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersons.Commands.SaveContactPerson
{
    public class SaveContactPersonContactHandler : IRequestHandler<SaveContactPersonCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SaveContactPersonContactHandler> _logger;

        public SaveContactPersonContactHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<SaveContactPersonContactHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> Handle(SaveContactPersonCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.Repository<ContactPerson>().Find(x => x.Name == request.Name
            && x.Surname == request.Surname);

            if (existing != null)
                return "Person already exists.";

            var contactPersonEntity = _mapper.Map<ContactPerson>(request);
            await _unitOfWork.Repository<ContactPerson>().Add(contactPersonEntity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Contact {contactPersonEntity.Name}, {contactPersonEntity.Surname} is successfully created.");

            return $"{contactPersonEntity.Name}, {contactPersonEntity.Surname}";
        }
    }
}
