using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;

namespace WebApplication2.Models
{
    public class BankContext : DbContext
    {

        public DbSet<UserAccounts> UserAccounts { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {



        }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(r => r.Id).IsUnique();
            modelBuilder.Entity<BankBranch>().Property(r => r.LocationName).IsRequired();
        }
    }
}
