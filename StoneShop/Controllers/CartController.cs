using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Models.ViewModels;
using StoneShop.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoneShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _database;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext database, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _database = database;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Index()  // список всех товаров
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
                User = _database.User.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = productList.ToList()
            };

            return View(productUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM productUserVM)
        {
            string pathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "templates" +  
                                    Path.DirectorySeparatorChar.ToString() + "Inquiry.html";  // путь к шаблону email-сообшщения

            var subject = "New Inquiry";
            string HtmlBody = "";

            using (StreamReader reader = System.IO.File.OpenText(pathToTemplate))  // загружаем код шаблона в тело сообщения
            {
                HtmlBody = reader.ReadToEnd();
            }

            StringBuilder  productListSB = new StringBuilder();
            foreach(var prod in productUserVM.ProductList)
            {
                productListSB.Append($" - Name:{prod.Name} <span syle='font-size:14px;'> (ID: {prod.Id})</span><br />");  // добавляем строки с товарами 
            }

            // по сути в HtmlBody есть скобки {} поэтому получается подставка агрументов под номера 
            string messageBody = string.Format(HtmlBody, productUserVM.User.FullName, productUserVM.User.PhoneNumber, productUserVM.User.Email, productListSB.ToString());
            await _emailSender.SendEmailAsync(WebConstants.AdminEmail, subject, messageBody);

            return RedirectToAction("InquiryConfiguration");
        }

        public IActionResult InquiryConfiguration()
        {
            HttpContext.Session.Clear();

            return View();
        }

    }
}
