using Microsoft.EntityFrameworkCore;
using WebCountry.Models;

namespace WebCountry.Data
{
    public class HumanResourceContext : DbContext
    {
        public HumanResourceContext(DbContextOptions options) : base(options) { }
        public DbSet<Country> Country { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}