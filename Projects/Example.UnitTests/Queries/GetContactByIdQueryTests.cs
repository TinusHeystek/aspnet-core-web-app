using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Example.App.CQS.Queries;
using Example.App.Data.Context;
using Example.App.Data.Models;
using Example.UnitTests.Factories;
using Moq;
using NUnit.Framework;

namespace Example.UnitTests.Queries
{
    [TestFixture]
    public class GetContactByIdQueryTests
    {
        [Test]
        public async Task GetContactById_Query_should_return_Contact_with_Id()
        {
            // Arrange
            var contextMock = new Mock<IContactsDbContext>();

            int id = 1;
            var contact = ContactFactory.GetContact();
            contact.Address = null;
            contact.Pets = new List<Pet>();
            contextMock.Setup(c => c.Contacts.FindAsync(id)).ReturnsAsync(contact);

            var query = new GetContactByIdQuery(id);
            var handler = new GetContactByIdQueryHandler(contextMock.Object);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            contextMock.Verify(c => c.Contacts.FindAsync(id), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(ContactFactory.Name, result.Name);
        }
    }
}
