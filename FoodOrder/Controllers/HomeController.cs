using FoodOrder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Castle.Core.Internal;
using Persistence.Services;

namespace FoodOrder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodOrderService _service;
        public HomeController(ILogger<HomeController> logger, IFoodOrderService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var favoutires = _service.GetFavorites();
            ViewBag.Favorites = favoutires;
            return View(_service.GetCategories());
        }

        public IActionResult Details(int id, string filter)
        {
            try
            {
                ViewData["CurrentFilter"] = filter;
                ViewData["CurrentCategoryName"] = _service.GetCategoryById(id).Name;

                var category = _service.GetCategoryById(id);
                var items = _service.GetItemsByCategoryId(id);
                if ((category == null) || items.IsNullOrEmpty())
                {
                    return NotFound();
                }

                if (!filter.IsNullOrEmpty())
                {
                    items = items.Where(s => s.Name.Contains(filter)).ToList();
                }

                return View(items);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}