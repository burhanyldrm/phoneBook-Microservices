using System;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactPersonList
{
    public class ContactPersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
