using Microsoft.EntityFrameworkCore;
using Tables;

namespace Crud.API.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

    }
}
