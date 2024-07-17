using CreditService.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Credit> Credit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasOne(p => p.Credit)
                .WithOne(p => p.Customer)
                .HasForeignKey<Customer>(p => p.IdCustomer);

            modelBuilder
                .Entity<Credit>()
                .HasOne(p => p.Customer)
                .WithOne(p => p.Credit)
                .HasForeignKey<Credit>(p => p.IdCustomer);
        }
    }
}
