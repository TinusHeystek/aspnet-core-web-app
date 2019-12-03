using Example.App.Shared.Models.View.Contact;
using Example.Shared.Core.Models;
using MediatR;

namespace Example.App.Shared.Models.Commands
{
    public class UpdateContactCommand: IRequest<CommandResult>
    {
        public ContactModel ContactModel { get; }

        public UpdateContactCommand(ContactModel contactModel)
        {
            ContactModel = contactModel;
        }
    }
}
