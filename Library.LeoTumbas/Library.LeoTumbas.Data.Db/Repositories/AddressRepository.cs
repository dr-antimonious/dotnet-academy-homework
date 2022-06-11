using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.LeoTumbas.Data.Db.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly DbSet<Address> _dbSet;

        public AddressRepository(IdentityDbContext<Person, IdentityRole<int>, int> dbContext)
        {
            _dbSet = dbContext.Set<Address>();
        }

        public async Task Create(Address entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IList<Address>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync<Address>();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync<Address>(address => address.Id == id);
        }

        public async Task Update(Address entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }
    }
}
