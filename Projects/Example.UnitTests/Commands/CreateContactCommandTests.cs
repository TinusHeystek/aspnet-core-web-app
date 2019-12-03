using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Commands;
using Example.App.Data.Context;
using Example.App.Data.Models;
using Example.App.Shared.Models.Commands;
using Example.App.Shared.Models.View.Contact;
using Example.UnitTests.Factories;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using ValidationContext = FluentValidation.ValidationContext;

namespace Example.UnitTests.Commands
{
    [TestFixture]
    public class CreateContactCommandTests
    {
        [Test]
        public async Task CreateContact_Command_should_return_new_Contact()
        {
            // Arrange
            var contextMock = new Mock<IContactsDbContext>();
            var validatorMock = new Mock<IValidator<ContactModel>>();
            var mapperMock = new Mock<IMapper>();

            int id = 1;
            var contactModel = GetContactModel(0);
            var contact = GetContact(1);

            contextMock.Setup(c => c.Contacts.AddAsync(contact, It.IsAny<CancellationToken>()));
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            mapperMock.Setup(m => m.Map<Contact>(contactModel)).Returns(contact);

            var command = new CreateContactCommand(contactModel);
            var handler = new CreateContactCommandHandler(contextMock.Object, validatorMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            contextMock.Verify(c => c.Contacts.AddAsync(contact, It.IsAny<CancellationToken>()), Times.Once);
            validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(id, result.Id);
        }

        private Contact GetContact(int id)
        {
            var contact = ContactFactory.GetContact();
            contact.Id = id;
            contact.Address.Id = id;
            contact.Pets.ForEach(p => p.Id = id);
            return contact;
        }

        private ContactModel GetContactModel(int id)
        {
            var contactModel = ContactFactory.GetContactModel();
            contactModel.Id = id;
            contactModel.Address.Id = id;
            contactModel.Pets.ForEach(p => p.Id = id);
            return contactModel;
        }
    }
}
