using Contact.Domain.Enums;
using MediatR;

namespace Contact.Application.Features.ContactPersonInfos.Commands.DeleteContactPersonInfo
{
    public class DeleteContactPersonInfoCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ContactInfoType ContactInfoType { get; set; }
    }
}
