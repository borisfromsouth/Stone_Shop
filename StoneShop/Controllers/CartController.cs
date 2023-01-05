using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Models.ViewModels;
using StoneShop.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace StoneShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _database;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);  // получаем данные в сессии
            }

            List<int> prodInCart = shoppingCartList.Select(u => u.ProductId).ToList();  // получаем только данные определенного поля 
            IEnumerable<Product> productList = _database.Product.Where(u => prodInCart.Contains(u.Id));  // получаем список продуктов по списку id-шников в корзине


            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction("Summary");
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set<List<ShoppingCart>>(WebConstants.SessionCart, shoppingCartList);

            return RedirectToAction("Index");
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = User.FindFirstValue(ClaimTypes.Name);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);  // получаем данные в сессии
            }

            List<int> prodInCart = shoppingCartList.Select(u => u.ProductId).ToList();  // получаем только данные определенного поля 
            IEnumerable<Product> productList = _database.Product.Where(u => prodInCart.Contains(u.Id));  // получаем список продуктов по списку id-шников в корзине


            ProductUserVM productUserVM = new ProductUserVM()
            {
                User = _database.User.FirstOrDefault(u => u.Id == userId)
            };

            return View(productUserVM);
        }
    }
}
