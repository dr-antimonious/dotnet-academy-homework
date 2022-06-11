using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Repositories;

namespace Library.LeoTumbas.Services
{
    public class DefaultPeopleService : IPeopleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DefaultPeopleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person?> CreatePerson(PersonValidator personValidator)
        {
            IList<Address>? addresses = await _unitOfWork.Addresses.GetAllAsync();

            Address? matchingAddress =
                addresses is null
                || addresses.Count.Equals(0)
                    ? null
                    : addresses.FirstOrDefault(address =>
                        address.Street.Equals(personValidator.Street)
                        && address.City.Equals(personValidator.City)
                        && address.Country.Equals(personValidator.Country));

            matchingAddress =
                matchingAddress is not null
                && matchingAddress.Equals(default(Address))
                    ? null
                    : matchingAddress;

            Address? address =
                addresses is null
                || addresses.Count.Equals(0)
                    ? new Address(
                        personValidator.Street,
                        personValidator.City,
                        personValidator.Country)
                    : matchingAddress;

            address ??= new Address(
                          personValidator.Street,
                          personValidator.City,
                          personValidator.Country);

            Person? person = new Person(
                personValidator.FirstName,
                personValidator.LastName,
                address);

            if (matchingAddress is null)
            {
                await _unitOfWork.Addresses.Create(address);
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.People.Create(person);
            await _unitOfWork.SaveChangesAsync();
            IList<Person>? people = await _unitOfWork.People.GetAllAsync();
            addresses = await _unitOfWork.Addresses.GetAllAsync();

            if (people is not null
                && !people.Count.Equals(0)
                && people.Contains(person)
                && addresses is not null
                && !addresses.Count.Equals(0)
                && addresses.Contains(address))
            {
                return person;
            }

            return null;
        }

        public async Task<IList<Person>?> GetAll()
        {
            IList<Person>? people = await _unitOfWork.People.GetAllAsync();
            IList<Address>? addresses = await _unitOfWork.Addresses.GetAllAsync();
            if (people is not null
                && !people.Count.Equals(0)
                && addresses is not null
                && !addresses.Count.Equals(0))
            {
                for (int index = 0; index < people.Count; index++)
                {
                    people[index].Address = addresses[people[index].AddressId - 1];
                }

                return people;
            }

            return null;
        }

        public async Task<IList<Person>?> GetByCityName(string city)
        {
            IList<Person>? people = await _unitOfWork.People.GetByCityNameAsync(city);
            IList<Address>? addresses = await _unitOfWork.Addresses.GetAllAsync();
            if (people is not null
                && !people.Count.Equals(0)
                && addresses is not null
                && !addresses.Count.Equals(0))
            {
                for (int index = 0; index < people.Count; index++)
                {
                    people[index].Address = addresses[people[index].AddressId - 1];
                }

                return people;
            }

            return null;
        }

        public async Task<Person?> GetById(int id)
        {
            Person? person = await _unitOfWork.People.GetByIdAsync(id);
            if (person is not null)
            {
                Address? address = await _unitOfWork.Addresses.GetByIdAsync(person.AddressId);
                if (address is not null)
                {
                    person.Address = address;
                    return person;
                }
            }

            return null;
        }
    }
}
