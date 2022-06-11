using Microsoft.AspNetCore.Identity;

namespace Library.LeoTumbas.Contracts.Entities
{
    public class Person : IdentityUser<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}";
        public int AddressId { get; set; } = default!;
        public virtual Address Address { get; set; } = default!;

        public Person(int id, string firstName, string lastName, int addressId, Address address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AddressId = addressId;
            Address = address;
        }

        public Person(int id, string firstName, string lastName, int addressId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AddressId = addressId;
        }

        public Person(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public Person(string firstName, string lastName, string email, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            UserName = $"{Guid.NewGuid()}";
        }
    }
}
