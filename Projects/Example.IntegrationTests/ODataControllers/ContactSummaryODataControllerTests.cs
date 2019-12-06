using System.Threading.Tasks;
using Example.App.Shared.Clients.ODataClients;
using Example.App.Shared.Interfaces.ODataClients;
using Example.IntegrationTests._SetUp;
using NUnit.Framework;

namespace Example.IntegrationTests.ODataControllers
{
    [TestFixture]
    public class ContactSummaryODataControllerTests
    {
        private readonly IContactSummaryODataClient _client;

        public ContactSummaryODataControllerTests()
        {
            _client = new ContactSummaryODataClient(ApiRoute.Base);
        }

        [Test]
        public async Task GetFakeNames_WhenCalled_Returns_FakeNames_OfCount()
        {
            var response = await _client.Get();

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task GenerateContacts_WhenCalled_Returns_Contacts_OfCount()
        {
            const int id = 2;
            var response = await _client.GetById(id);

            Assert.IsNotNull(response);
            Assert.AreEqual(id, response.Id);
        }
    }
}