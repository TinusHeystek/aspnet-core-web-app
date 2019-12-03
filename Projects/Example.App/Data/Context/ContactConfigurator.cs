using Example.App.Data.Models;
using Example.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.App.Data.Context
{
    internal class ContactConfigurator : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasOne(c => c.Address)
                .WithOne(ad => ad.Contact)
                .HasForeignKey<Address>(ad => ad.ContactId);

            builder.HasMany(x => x.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            builder.Property(p => p.Weight)
                .HasColumnType(EntityTypeConfigurationConst.DecimalColumnType_18_2);
        }
    }
}