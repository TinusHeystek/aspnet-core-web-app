using System;
using System.Collections.Generic;
using Example.App.Shared.Enums;
using Example.Shared.Core.Models;

namespace Example.App.Shared.Models.View.Contact
{
    public class ContactModel : IId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirthUtc { get; set; }
        public string PhoneNumber { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public string EyeColor { get; set; }
        public string Sport { get; set; }
        public Gender Gender { get; set; }

        public AddressModel Address { get; set; }
        public List<PetModel> Pets { get; set; }
    }
}
