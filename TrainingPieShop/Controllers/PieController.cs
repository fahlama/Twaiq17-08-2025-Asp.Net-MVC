using Microsoft.AspNetCore.Mvc;
using TrainingPieShop.Models;
using TrainingPieShop.ViewModels;

namespace TrainingPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }
        //public IActionResult List()
        //{
        //    // ViewBag.CurrentCategory = "Cheese cakes";
        //    PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
        //    return View(pieListViewModel);
        //}

        public IActionResult List(string categoryName)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;
            if (string.IsNullOrEmpty(categoryName))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All Pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p=>p.Category.CategoryName==categoryName).OrderBy(p => p.PieId);    
                currentCategory = categoryName;
            }
            // ViewBag.CurrentCategory = "Cheese cakes";
            PieListViewModel pieListViewModel = new PieListViewModel(pies, currentCategory);
            return View(pieListViewModel);
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
