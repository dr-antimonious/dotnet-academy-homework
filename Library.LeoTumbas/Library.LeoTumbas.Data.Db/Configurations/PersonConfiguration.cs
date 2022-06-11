using Library.LeoTumbas.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.LeoTumbas.Data.Db.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);

            builder.Property(person => person.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(person => person.LastName)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(person => person.Address)
                .WithMany(address => address.People)
                .HasForeignKey(people => people.AddressId);
        }
    }
}
