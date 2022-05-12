using Contact.Domain.Enums;
using MediatR;
using System;

namespace Contact.Application.Features.ContactPersonInfos.Commands.SaveContactPersonInfo
{
    public class SaveContactPersonInfoCommand : IRequest<bool>
    {
        public Guid ContactPersonId { get; set; }
        public ContactInfoType Type { get; set; }
        public string Info { get; set; }
    }
}
