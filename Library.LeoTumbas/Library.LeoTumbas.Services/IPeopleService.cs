using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Entities;

namespace Library.LeoTumbas.Services
{
    public interface IPeopleService
    {
        Task<IList<Person>?> GetAll();
        Task<Person?> GetById(int id);
        Task<IList<Person>?> GetByCityName(string city);
        Task<Person?> CreatePerson(PersonValidator personValidator);
    }
}
