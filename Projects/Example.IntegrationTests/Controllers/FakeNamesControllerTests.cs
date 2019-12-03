using System.Threading.Tasks;
using Example.App.Shared.Clients.ApiClients;
using Example.App.Shared.Interfaces.ApiClients;
using Example.IntegrationTests._SetUp;
using NUnit.Framework;

namespace Example.IntegrationTests.Controllers
{
    [TestFixture]
    public class FakeNamesControllerTests
    {
        private IFakeNameClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new FakeNameClient(ApiRoute.Base);
        }


        [Test]
        public async Task GetFakeNames_WhenCalled_Returns_FakeNames_OfCount()
        {
            var response = await _client.GetFakeNames(2);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.FakeNamesCount, 2);
        }

        [Test]
        public async Task GenerateContacts_WhenCalled_Returns_Contacts_OfCount()
        {
            var response = await _client.GenerateContacts(2);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.ContactCount, 2);
        }
    }
}