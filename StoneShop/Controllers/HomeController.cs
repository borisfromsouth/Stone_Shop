using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Models.ViewModels;
using StoneShop.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StoneShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _bataBase;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext bataBase)
        {
            _logger = logger;
            _bataBase = bataBase;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _bataBase.Product.Include(u => u.Category).Include(u => u.ApplicationType),
                Categorys = _bataBase.Category
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _bataBase.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            return View(detailsVM);
        }

        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCart = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&  // проверка на заполненность корзины
               HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0 )
            {
                shoppingCart = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart); // если что-то есть то забираем на обработку 
            }
            shoppingCart.Add(new ShoppingCart { ProductId = id });  // добавляем товар в корзину
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCart);  // сохраняем корзину в сессии
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
