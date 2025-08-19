
using Microsoft.EntityFrameworkCore;

namespace TrainingPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly ShopDBContext _dbContext;
        public PieRepository(ShopDBContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _dbContext.pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _dbContext.pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _dbContext.pies.FirstOrDefault(c=>c.PieId == pieId); 
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
