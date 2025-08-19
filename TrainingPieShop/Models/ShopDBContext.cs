using Microsoft.EntityFrameworkCore;

namespace TrainingPieShop.Models
{
    public class ShopDBContext:DbContext
    {
        public ShopDBContext(DbContextOptions<ShopDBContext> options):base(options)

        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> pies { get; set; }
    }
}
