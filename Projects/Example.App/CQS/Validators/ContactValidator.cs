using System.Threading;
using System.Threading.Tasks;
using Example.App.Shared.Models.View.Contact;
using FluentValidation;
using FluentValidation.Results;

namespace Example.App.CQS.Validators
{
    public class ContactValidator : AbstractValidator<ContactModel>
    {
        public const string CreateRuleSet = "Create";
        public const string UpdateRuleSet = "Update";
        public const string FindRuleSet = "Find";

        public ContactValidator()
        {
            RuleSet(CreateRuleSet, () =>
            {
                RuleFor(contact => contact.Id).Must(x => x == 0);
                RuleFor(contact => contact.Name).NotNull().NotEmpty();
            });

            RuleSet(UpdateRuleSet, () =>
            {
                RuleFor(contact => contact.Id).Must(x => x != 0);
                RuleFor(contact => contact.Name).NotNull().NotEmpty();
            });

            RuleSet(FindRuleSet, () =>
            {
                RuleFor(contact => contact.Id).Must(x => x != 0).WithMessage("Not Found.");
            });
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<ContactModel> context, CancellationToken cancellation = new CancellationToken())
        {
            var type = context.InstanceToValidate.GetType();
            return context.InstanceToValidate == null 
                ? new ValidationResult(new[] { new ValidationFailure($"{type}", $"{type} cannot be null") })
                : await base.ValidateAsync(context, cancellation);
        }
    }
}
