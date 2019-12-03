using System.Collections.Generic;
using AutoMapper;
using Example.App.Data.Models;
using Example.App.Shared.Models.View;
using Example.App.Shared.Models.View.Contact;

namespace Example.App.Mappings
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactSummary>()
                .ForMember(d => d.Initials, opt => opt.MapFrom(s => s.Name.Substring(0, 1)))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => (int) s.Gender))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address.AddressInfo));

            CreateMap<IEnumerable<Contact>, IEnumerable<ContactSummary>>();

            CreateMap<Contact, ContactModel>();
            CreateMap<ContactModel, Contact>();

            CreateMap<Address, AddressModel>();
            CreateMap<AddressModel, Address>();

            CreateMap<Pet, PetModel>();
            CreateMap<PetModel, Pet>();
        }
    }
}
