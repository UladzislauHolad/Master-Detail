using System.Data.Entity;

namespace Step_4.Models
{
    public class ProductContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ProductContext() : base("ProductContext")
        {

        }
    }
}