using System.Reflection;
using Library.LeoTumbas.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.LeoTumbas.Data.Db.Configurations
{
    public class PeopleDbContext : IdentityDbContext<Person, IdentityRole<int>, int>
    {
        public DbSet<Person> People { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;

        public PeopleDbContext(DbContextOptions<PeopleDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public PeopleDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
