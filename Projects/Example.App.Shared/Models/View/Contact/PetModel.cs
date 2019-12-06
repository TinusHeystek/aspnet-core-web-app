using Example.Shared.Core.Models;

namespace Example.App.Shared.Models.View.Contact
{
    public class PetModel : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int OwnerId { get; set; }
        public ContactModel Owner { get; set; }
    }
}