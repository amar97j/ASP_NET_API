using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }
       
        
    }
}
