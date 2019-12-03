using Example.App.CQS.Validators;
using Example.App.Shared.Models.View.Contact;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Example.UnitTests.Validators
{
    [TestFixture]
    public class ContactValidatorTests
    {
        private ContactValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ContactValidator();
        }

        [Test]
        public void CreateRuleSet_Should_have_error_when()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 1, ContactValidator.CreateRuleSet);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string, ContactValidator.CreateRuleSet);
        }

        [Test]
        public void CreateRuleSet_Should_not_have_error_when()
        {
            var contact = new ContactModel {Id = 0, Name = "Jeremy"};
            _validator.ShouldNotHaveValidationErrorFor(x => x.Id, contact, ContactValidator.CreateRuleSet);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, contact, ContactValidator.CreateRuleSet);
        }

        [Test]
        public void UpdateRuleSet_Should_have_error_when()
        {
            var contact = new ContactModel {Id = 0, Name = ""};
            var result = _validator.TestValidate(contact, ContactValidator.UpdateRuleSet);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void UpdateRuleSet_Should_not_have_error_when()
        {
            var contact = new ContactModel {Id = 1, Name = "Jeremy"};
            var result = _validator.TestValidate(contact, ContactValidator.UpdateRuleSet);

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}