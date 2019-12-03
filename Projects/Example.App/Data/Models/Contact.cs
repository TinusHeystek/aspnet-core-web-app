using System;
using System.Collections.Generic;
using Example.App.Shared.Enums;

namespace Example.App.Data.Models
{
    public class Contact
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

        public Address Address { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
