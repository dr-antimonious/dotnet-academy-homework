namespace Library.LeoTumbas.Contracts.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public virtual ICollection<Person> People { get; set; }

        public Address(int id, string street, string city, string country)
        {
            Id = id;
            Street = street;
            City = city;
            Country = country;
            People = new HashSet<Person>();
        }

        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
            People = new HashSet<Person>();
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {Country}";
        }
    }
}
