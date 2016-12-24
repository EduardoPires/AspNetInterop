using System.Data.Entity;

namespace AspNetInterop.UI.MVC5.Models
{
    public class AspNetInteropContext : DbContext
    {
        public AspNetInteropContext() 
            : base("DefaultConnection")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}