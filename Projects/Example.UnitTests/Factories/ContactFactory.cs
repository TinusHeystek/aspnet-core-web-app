using System;
using System.Collections.Generic;
using Example.App.Data.Models;
using Example.App.Shared.Enums;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;

namespace Example.UnitTests.Factories
{
    public static class ContactFactory
    {
        public const int ContactId = 1;
        public const string Name = "Dillan Klein";
        public const string Initials = "D";
        public static readonly DateTime DateOfBirthUtc = new DateTime(1997, 10, 14, 16, 30, 50, DateTimeKind.Utc);
        public const string PhoneNumber = "012-555-1234";
        public const int Height = 207;
        public const decimal Weight = 99.60m;
        public const string EyeColor = "Amber";
        public const string Sport = "Swimming";
        public static readonly Gender Gender = Gender.Male;

        public const int AddressId = 2;
        public const string AddressInfo = "xxx";
        public const decimal AddressLatitude = -28.90358764m;
        public const decimal AddressLongitude = 131.87649035m;

        public const int PetId = 3;
        public const string PetName = "Ward";
        public const string PetType = "Lizzard";

        public static Contact GetContact()
        {
            return new Contact
            {
                Id = ContactId,
                Name = Name,
                DateOfBirthUtc = DateOfBirthUtc,
                PhoneNumber = PhoneNumber,
                Height = Height,
                Weight = Weight,
                EyeColor = EyeColor,
                Sport = Sport,
                Gender = Gender,
                Address = new Address
                {
                    Id = AddressId,
                    AddressInfo = AddressInfo,
                    Latitude = AddressLatitude,
                    Longitude = AddressLongitude,
                    ContactId = ContactId
                },
                Pets = new List<Pet>
                {
                    new Pet
                    {
                        Id = PetId,
                        Name = PetName,
                        Type = PetType,
                        OwnerId = ContactId
                    }
                }
            };
        }

        public static ContactModel GetContactModel()
        {
            return new ContactModel
            {
                Id = ContactId,
                Name = Name,
                DateOfBirthUtc = DateOfBirthUtc,
                PhoneNumber = PhoneNumber,
                Height = Height,
                Weight = Weight,
                EyeColor = EyeColor,
                Sport = Sport,
                Gender = Gender,
                Address = new AddressModel
                {
                    Id = AddressId,
                    AddressInfo = AddressInfo,
                    Latitude = AddressLatitude,
                    Longitude = AddressLongitude,
                    ContactId = ContactId
                },
                Pets = new List<PetModel>
                {
                    new PetModel
                    {
                        Id = PetId,
                        Name = PetName,
                        Type = PetType,
                        OwnerId = ContactId
                    }
                }
            };
        }

        public static ContactSummary GetContactSummary()
        {
            return new ContactSummary
            {
                Id = ContactId,
                Name = Name,
                Initials = Initials,
                EyeColor = EyeColor,
                Sport = Sport,
                Gender = (int)Gender,
                Address = AddressInfo
            };
        }

        public static Contact GetContact(int id)
        {
            var contact = GetContact();
            contact.Id = id;
            contact.Address.Id = id;
            contact.Pets.ForEach(p => p.Id = id);
            return contact;
        }

        public static ContactModel GetContactModel(int id)
        {
            var contactModel = GetContactModel();
            contactModel.Id = id;
            contactModel.Address.Id = id;
            contactModel.Pets.ForEach(p => p.Id = id);
            return contactModel;
        }
    }
}