using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Models;
using MediatR;

namespace Example.App.Shared.Models.Commands
{
    public class CreateContactCommand: IRequest<CommandResult>
    {
        public ContactModel ContactModel { get; }

        public CreateContactCommand(ContactModel contactModel)
        {
            ContactModel = contactModel;
        }
    }
}
