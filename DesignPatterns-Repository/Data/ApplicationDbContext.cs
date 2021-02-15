using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DesignPatterns_Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            LoadJson(builder);
            
            base.OnModelCreating(builder);
        }

        private void LoadJson(ModelBuilder builder)
        {
            string path = @"..\DesignPatterns-Repository\Data\JsonFiles\Customers.json";

            if (File.Exists(path))
            {
                var leagues = JsonConvert.DeserializeObject<Customer[]>
                   (File.ReadAllText(path));

                builder.Entity<Customer>().HasData(leagues);
            }
        }
    }
}
