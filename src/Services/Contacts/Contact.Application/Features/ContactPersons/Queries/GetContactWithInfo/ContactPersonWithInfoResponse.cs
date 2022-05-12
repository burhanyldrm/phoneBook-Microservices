using Contact.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Contact.Application.Features.ContactPersons.Queries.GetContactWithInfo
{
    public class ContactPersonWithInfoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ContactInfoResponse> ContactInfos { get; set; }
    }

    public class ContactInfoResponse
    {
        public ContactInfoType Type { get; set; }
        public string Info { get; set; }
    }
}
