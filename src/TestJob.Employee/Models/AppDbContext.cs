using Microsoft.Data.Entity;

namespace TestJob.Employee.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Workman> Workmen { get; set; }
    }
}
