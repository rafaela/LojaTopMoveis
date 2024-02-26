using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Topmoveis.Model;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace Topmoveis.Data
{
    public class LojaContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<IdentityUser>
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
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Subcategory> Subcategories{ get; set; }
        public DbSet<Freight> Freights { get; set; }
        public DbSet<SubcategoriesProduct> SubcategoriesProducts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductsSale> ProductsSales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(a => a.Id);

            modelBuilder.Entity<IdentityUserLogin>().HasKey(a => a.UserId);

            modelBuilder.Entity<IdentityUserRole>().HasKey(a => a.UserId);

        }

        internal object FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
