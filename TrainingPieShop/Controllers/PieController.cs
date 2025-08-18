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
        public IActionResult List()
        {
            // ViewBag.CurrentCategory = "Cheese cakes";
            PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
            return View(pieListViewModel);
        }
    }
}
