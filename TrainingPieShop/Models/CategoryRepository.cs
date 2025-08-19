
namespace TrainingPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDBContext _dbContext;
        public CategoryRepository(ShopDBContext context)
        {
            _dbContext=context;
        }
        public IEnumerable<Category> AllCategories => _dbContext.Categories.OrderBy(c => c.CategoryName);

    }
}
