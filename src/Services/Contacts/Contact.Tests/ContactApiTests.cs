using Contact.Application.Features.ContactPersonInfos.Commands.SaveContactPersonInfo;
using Contact.Application.Features.ContactPersons.Commands.SaveContactPerson;
using Contact.Application.Features.ContactPersons.Queries.GetContactPersonList;
using Contact.Domain.ContactPersonAggregate;
using Contact.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Tests
{
    public class Tests : TestBase
    {

        [Test]
        public async Task CreateContact()
        {
            var mockLogger = new Mock<ILogger<SaveContactPersonContactHandler>>();

            var request = new SaveContactPersonCommand { Name = "TestUser", Surname = "TestUser", Firm = "Test" };
            var sut = new SaveContactPersonContactHandler(UnitOfWork, Mapper, mockLogger.Object);
            var expectedMessages = new List<string> { "TestUser, TestUser", "Person already exists." };

            var actual = await sut.Handle(request, new CancellationToken());

            Assert.True(expectedMessages.Contains(actual));
        }

        [Test]
        public async Task CreateContactInfo()
        {
            var exists = await UnitOfWork.Repository<ContactPerson>().FindByProperties(x => x.Name == "TestUser" && x.Surname == "TestUser");

            if (exists == null)
            {
                await CreateContact();
                exists = await UnitOfWork.Repository<ContactPerson>().FindByProperties(x => x.Name == "TestUser" && x.Surname == "TestUser");
            }

            var mockLogger = new Mock<ILogger<SaveContactPersonInfoHandler>>();
            var request = new SaveContactPersonInfoCommand
            {
                ContactPersonId = exists.Id,
                Type = ContactInfoType.Phone,
                Info = "05347277371"
            };
            var sut = new SaveContactPersonInfoHandler(UnitOfWork, Mapper, mockLogger.Object);
            var actual = await sut.Handle(request, new CancellationToken());

            Assert.AreEqual(actual, true);
        }

        [Test]
        public async Task GetContactList()
        {
            var contactListCount = await UnitOfWork.Repository<ContactPerson>().Count();
            var request = new GetContactPersonListQuery();
            var sut = new GetContactPersonListQueryHandler(UnitOfWork, Mapper);
            var actual = await sut.Handle(request, new CancellationToken());

            Assert.AreEqual(actual.Count, contactListCount);
        }
    }
}