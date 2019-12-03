using System;
using System.Linq;
using AutoMapper;
using Example.App.Common;
using Example.App.Data.Models;
using Example.App.Shared.Enums;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;

namespace Example.App.Mappings
{
    public class FakeNameProfile : Profile
    {
        public FakeNameProfile()
        {
            CreateMap<FakeName, ContactModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => 0))
                .ForMember(d => d.Address,
                    opt => opt.MapFrom(s => new Address
                    {
                        AddressInfo = s.Address,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude
                    }))
                .ForMember(d => d.Pets, opt => opt.Ignore())
                .ForMember(d => d.Pets,
                    opt => opt.MapFrom(s => s.Company
                        .Split(new[] {" ", "-"}, StringSplitOptions.RemoveEmptyEntries)
                        .Where(name => name.Length >= 4)
                        .Select(name => new Pet
                        {
                            Name = name,
                            Type = PetTypes.GetRandom()
                        }).ToList()))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s =>
                    s.Pict.EndsWith("female", StringComparison.InvariantCultureIgnoreCase)
                        ? Gender.Female
                        : Gender.Male))
                .ForMember(d => d.Height, opt => opt.MapFrom(s => (int) s.Height));
        }
    }
}