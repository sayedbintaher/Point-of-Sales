using Microsoft.EntityFrameworkCore;
using PosAPI.Models;

namespace PosAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItems> TransactionItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}
