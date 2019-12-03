using Example.App.Data.Models;
using Example.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.App.Data.Context
{
    internal class AddressConfigurator : IEntityTypeConfiguration<Address>
    {
        
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.Property(p => p.Latitude)
                .HasColumnType(EntityTypeConfigurationConst.DecimalColumnType_18_6);

            builder.Property(p => p.Longitude)
                .HasColumnType(EntityTypeConfigurationConst.DecimalColumnType_18_6);
        }
    }
}