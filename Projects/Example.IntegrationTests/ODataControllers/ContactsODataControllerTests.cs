using System.Threading.Tasks;
using Example.App.Shared.Clients.ODataClients;
using Example.App.Shared.Interfaces.ODataClients;
using Example.IntegrationTests._SetUp;
using NUnit.Framework;

namespace Example.IntegrationTests.ODataControllers
{
    [TestFixture]
    public class ContactsODataControllerTests
    {
        private readonly IContactsODataClient _client;

        public ContactsODataControllerTests()
        {
            _client = new ContactsODataClient(ApiRoute.Base);
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
            var response = await _client.GetByKey(1);

            Assert.IsNotNull(response);
        }
    }
}