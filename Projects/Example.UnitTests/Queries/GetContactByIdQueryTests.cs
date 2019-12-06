using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Example.App.CQS.Queries;
using Example.App.Mappings;
using Example.UnitTests.Data;
using Example.UnitTests.Extensions;
using Example.UnitTests.Factories;
using NUnit.Framework;

namespace Example.UnitTests.Queries
{
    [TestFixture]
    public class GetContactByIdQueryTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = MapperExtensions.CreateMapper(new ContactProfile());
        }

        [Test]
        public async Task GetContactById_Query_should_return_Contact_with_Id()
        {
            // Arrange
            int id = 1;
            var context = DbContextFactory.CreateContext();
            var query = new GetContactModelByIdQuery(id);
            var handler = new GetContactModelByIdQueryHandler(context, _mapper);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(ContactFactory.Name, result.Name);
        }
    }
}
