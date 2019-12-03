using AutoMapper;
using Example.App.Mappings;
using Example.App.Shared.Models.View;
using Example.UnitTests.Factories;
using NUnit.Framework;

namespace Example.UnitTests.Mappings
{
    [TestFixture]
    public class ContactProfileTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ContactProfile>();
            });
            
            _mapper = config.CreateMapper();
        }
    
        [Test]
        public void AutoMapper_Configuration_IsValid() 
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapper_Contact_To_ContactSummary_IsValid()
        {
            var contact = ContactFactory.GetContact();
            var contactSummary = _mapper.Map<ContactSummary>(contact);

            Assert.AreEqual(ContactFactory.ContactId, contactSummary.Id);
            Assert.AreEqual(ContactFactory.Name, contactSummary.Name);
            Assert.AreEqual(ContactFactory.Initials, contactSummary.Initials);
            Assert.AreEqual(ContactFactory.EyeColor, contactSummary.EyeColor);
            Assert.AreEqual(ContactFactory.Sport, contactSummary.Sport);
            Assert.AreEqual((int)ContactFactory.Gender, contactSummary.Gender);
            Assert.AreEqual(ContactFactory.AddressInfo, contactSummary.Address);
        }
    }
}
