using Contact.Domain.Common;
using Contact.Domain.Enums;
using System;

namespace Contact.Domain.ContactPersonAggregate
{
    public class ContactPersonInfo : EntityBase
    {
        public Guid ContactPersonId { get; protected set; }
        public ContactInfoType Type { get; protected set; }
        public string Info { get; protected set; }

        public ContactPersonInfo()
        {
        }
        public ContactPersonInfo(Guid contactPersonId, ContactInfoType type, string info)
        {
            ContactPersonId = contactPersonId;
            Type = type;
            Info = info;
        }

        public void SetContactInfo(ContactInfoType type, string info)
        {
            Type = type;
            Info = info;
        }

        public void SetInfo(string info)
        {
            Info = info;
        }

        public void SetType(ContactInfoType type)
        {
            Type = type;
        }
    }
}