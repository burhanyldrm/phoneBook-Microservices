using MediatR;
using System;

namespace Contact.Application.Features.ContactPersons.Commands.DeleteContactPerson
{
    public class DeleteContactPersonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
