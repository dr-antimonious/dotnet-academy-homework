using Library.LeoTumbas.Contracts.Entities;

namespace Library.LeoTumbas.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        IPeopleRepository<Person> People { get; }
        IRepository<Address> Addresses { get; }

        Task SaveChangesAsync();
    }
}
