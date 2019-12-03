using System.Threading.Tasks;
using Example.App.Shared.Clients.ApiClients;
using Example.App.Shared.Interfaces.ApiClients;
using Example.IntegrationTests._SetUp;
using NUnit.Framework;

namespace Example.IntegrationTests.Controllers
{
    [TestFixture]
    public class ContactControllerTests
    {
        private IContactClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new ContactClient(ApiRoute.Base);
        }

        [Test]
        public async Task Get_WhenCalled_Returns_PagedResult_ContactModel()
        {
            var response = await _client.Get(1, 10);

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task GenerateContacts_WhenCalled_Should_Add_Contacts_To_Database()
        {
            var originalCount = await _client.Count();

            int generateCount = 2;
            var response = await _client.GenerateContacts(generateCount);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(generateCount, response.SeedCount);

            var currentCount = await _client.Count();
            Assert.AreEqual(originalCount + generateCount, currentCount);
        }
    }
}