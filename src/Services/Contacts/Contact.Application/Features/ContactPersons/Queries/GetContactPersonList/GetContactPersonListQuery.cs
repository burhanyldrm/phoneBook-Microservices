using MediatR;
using System.Collections.Generic;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactPersonList
{
    public class GetContactPersonListQuery : IRequest<List<ContactPersonResponse>>
    {
    }
}
