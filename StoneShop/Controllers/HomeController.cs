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
        private readonly Service _service;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext bataBase, Service service)
        {
            _logger = logger;
            _bataBase = bataBase;
            _service = service;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _bataBase.Product.Include(u => u.Category).Include(u => u.ApplicationType),
                Categorys = _bataBase.Category
            };
            return View(/*homeVM*/);
        }

        public IActionResult SendEmailDefault()
        {
            _service.SendEmailDefault();
            return RedirectToAction("Index");
        }

        public IActionResult SendEmailCustom()
        {
            _service.SendEmailCustom();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&  // проверка на заполненность корзины
               HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart); // если что-то есть то забираем на обработку 
            }


            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _bataBase.Product.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };

            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                }
            }

            return View(detailsVM);
        }

        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&  // проверка на заполненность корзины
               HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0 )
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart); // если что-то есть то забираем на обработку 
            }
            shoppingCartList.Add(new ShoppingCart { ProductId = id });  // добавляем товар в корзину
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);  // сохраняем корзину в сессии
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart (int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null &&  
               HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart); 
            }

            var itemToRemove = shoppingCartList.SingleOrDefault(u => u.ProductId == id);  // вытягиваем нужный объект
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);  // удаляем объект если не пустой 
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);  // сохраняем корзину в сессии
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
