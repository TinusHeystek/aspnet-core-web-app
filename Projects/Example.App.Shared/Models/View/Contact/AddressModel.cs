using Example.Shared.Core.Models;

namespace Example.App.Shared.Models.View.Contact
{
    public class AddressModel : IId
    {
        public int Id { get; set; }
        public string AddressInfo { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public int ContactId { get; set; }
        public ContactModel Contact { get; set; }
    }
}