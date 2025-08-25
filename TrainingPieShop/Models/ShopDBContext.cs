using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TrainingPieShop.Models
{
    public class ShopDBContext:IdentityDbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options):base(options)

        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
