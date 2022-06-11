using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.LeoTumbas.Data.Db.Repositories
{
    public class PeopleUnitOfWork : IUnitOfWork
    {
        private readonly IPeopleRepository<Person>? _people;
        private readonly IRepository<Address>? _addresses;
        private readonly IdentityDbContext<Person, IdentityRole<int>, int> _dbContext;

        public PeopleUnitOfWork(IdentityDbContext<Person, IdentityRole<int>, int> dbContext)
        {
            _dbContext = dbContext;
        }

        public IPeopleRepository<Person> People =>
            _people is not null && _people.GetType().Equals(typeof(PeopleRepository))
            ? _people : new PeopleRepository(_dbContext);

        public IRepository<Address> Addresses =>
            _addresses is not null && _addresses.GetType().Equals(typeof(AddressRepository))
            ? _addresses : new AddressRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
