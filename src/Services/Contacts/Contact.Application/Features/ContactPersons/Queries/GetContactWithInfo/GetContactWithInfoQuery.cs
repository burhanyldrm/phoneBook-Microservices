using MediatR;
using System;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactWithInfo
{
    public class GetContactWithInfoQuery : IRequest<ContactPersonWithInfoResponse>
    {
        public Guid ContactId { get; set; }

        public GetContactWithInfoQuery(Guid id)
        {
            ContactId = id;
        }
    }
}
