using Contact.Domain.Common;
using Contact.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Contact.Domain.ContactPersonAggregate
{
    public class ContactPerson : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; protected set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; protected set; }
        [Required]
        [StringLength(250)]
        public string Firm { get; protected set; }
        private readonly List<ContactPersonInfo> _contactPersonInfos;
        public IReadOnlyCollection<ContactPersonInfo> ContactPersonInfos => _contactPersonInfos;
        public ContactPerson()
        {
            _contactPersonInfos = new List<ContactPersonInfo>();
        }

        public ContactPerson(string name, string surname, string firm)
        {
            Name = name;
            Surname = surname;
            Firm = firm;
        }

        public void SetContactPerson(string name, string surname, string firm)
        {
            Name = name;
            Surname = surname;
            Firm = firm;
        }

        public void VerifyOrAddContactInfo(ContactPersonInfo contactInfo)
        {
            var exist = _contactPersonInfos.FirstOrDefault(x => x.Type == contactInfo.Type);

            if (exist == null)
                _contactPersonInfos.Add(new ContactPersonInfo(Id, contactInfo.Type, contactInfo.Info));
            else
                exist.SetInfo(contactInfo.Info);
        }

        public void DeleteContactInfo(ContactInfoType type)
        {
            var exist = _contactPersonInfos.FirstOrDefault(x => x.Type == type);

            if (exist == null)
                throw new ArgumentNullException(nameof(type));

            _contactPersonInfos.Remove(exist);
        }

        public void DeleteAllContactInfos()
        {
            _contactPersonInfos.Clear();
        }
    }
}
