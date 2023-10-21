using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Topmoveis.Model;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Topmoveis.Data
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }

        internal object FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
