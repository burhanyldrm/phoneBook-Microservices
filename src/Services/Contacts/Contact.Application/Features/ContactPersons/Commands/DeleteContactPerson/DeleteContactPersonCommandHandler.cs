using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Exceptions;
using Contact.Domain.ContactPersonAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.ContactPersons.Commands.DeleteContactPerson
{
    public class DeleteContactPersonCommandHandler : IRequestHandler<DeleteContactPersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteContactPersonCommandHandler> _logger;

        public DeleteContactPersonCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<DeleteContactPersonCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteContactPersonCommand request, CancellationToken cancellationToken)
        {
            var contactToDelete = await _unitOfWork.Repository<ContactPerson>().FindByProperties(x => x.Id == request.Id, "ContactPersonInfos");

            if (contactToDelete == null)
                throw new NotFoundException(nameof(ContactPerson), request.Id);

            contactToDelete.DeleteAllContactInfos();

            _unitOfWork.Repository<ContactPerson>().Delete(contactToDelete);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"Contact Person {contactToDelete.Id} is successfully deleted.");

            return Unit.Value;

        }
    }
}
