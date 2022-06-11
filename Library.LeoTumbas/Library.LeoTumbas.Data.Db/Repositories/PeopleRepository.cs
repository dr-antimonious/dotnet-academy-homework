using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.LeoTumbas.Data.Db.Repositories
{
    public class PeopleRepository : IPeopleRepository<Person>
    {
        private readonly DbSet<Person> _dbSet;

        public PeopleRepository(IdentityDbContext<Person, IdentityRole<int>, int> dbContext)
        {
            _dbSet = dbContext.Set<Person>();
        }

        public async Task Create(Person entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IList<Person>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync<Person>();
        }

        public async Task<IList<Person>?> GetByCityNameAsync(string city)
        {
            return await _dbSet.Where(person => person.Address.City == city).ToListAsync<Person>();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync<Person>(person => person.Id == id);
        }

        public async Task Update(Person entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }
    }
}
