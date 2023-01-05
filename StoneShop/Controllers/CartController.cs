using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Utility;
using System.Collections.Generic;
using System.Linq;

namespace StoneShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _database;

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
    }
}
