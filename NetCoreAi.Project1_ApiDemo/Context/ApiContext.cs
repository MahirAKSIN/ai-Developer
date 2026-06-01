using Microsoft.EntityFrameworkCore;
using NetCoreAi.Project1_ApiDemo.Entities;

namespace NetCoreAi.Project1_ApiDemo.Context
{
    public class ApiContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-P5Q4357;initial catalog=ApiAIDb; integrated security=true;TrustServerCertificate=True");
        }
        public DbSet<Customer> Customers { get; set; }
    }

}
