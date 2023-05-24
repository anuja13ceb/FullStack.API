using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.DatabaseContext
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
