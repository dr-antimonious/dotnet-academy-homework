using Library.LeoTumbas.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.LeoTumbas.Data.Db.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);

            builder.Property(address => address.Street)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(address => address.City)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(address => address.Country)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
