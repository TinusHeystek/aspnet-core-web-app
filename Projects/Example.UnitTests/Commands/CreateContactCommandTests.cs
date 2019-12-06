using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Commands;
using Example.App.CQS.Validators;
using Example.App.Mappings;
using Example.App.Shared.Models.Commands;
using Example.UnitTests.Data;
using Example.UnitTests.Extensions;
using Example.UnitTests.Factories;
using NUnit.Framework;

namespace Example.UnitTests.Commands
{
    [TestFixture]
    public class CreateContactCommandTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = MapperExtensions.CreateMapper(new ContactProfile());
        }

        [Test]
        public async Task CreateContact_Command_should_return_new_Contact()
        {
            // Arrange
            var context = DbContextFactory.CreateContext();
            var validator = new ContactValidator();

            var contactModel = ContactFactory.GetContactModel(0);

            var command = new CreateContactCommand(contactModel);
            var handler = new CreateContactCommandHandler(context, validator, _mapper);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Id > 0);
        }
    }
}
