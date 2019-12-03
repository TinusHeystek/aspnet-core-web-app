using System.Collections.Generic;
using Example.App.Shared.Models.View.Contact;
using MediatR;

namespace Example.App.Shared.Models.Commands
{
    public class GenerateContactsCommand: IRequest<GenerateContactResult>
    {
        public int Count { get; }

        public GenerateContactsCommand(int count)
        {
            Count = count;
        }
    }

    public class GenerateContactResult
    {
        public List<ContactModel> Contacts { get; set; }
        public int ContactCount { get; set; }
        public int PetCount { get; set; }
    }

}
