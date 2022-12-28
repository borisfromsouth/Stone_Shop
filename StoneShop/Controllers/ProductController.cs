using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace StoneShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dataBase;

        public ProductController(ApplicationDbContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _dataBase.Product;

            foreach (var obj in objList)
            {
                obj.Category = _dataBase.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            }

            return View(objList);
        }

        // операция GET показывает формочку
        [HttpGet]
        public IActionResult Upsert(int? id)  // Upsert - общий метод для создания и редактирования
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _dataBase.Category.Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;  // ViewBag передает данные из контроллера в предстиавоение, но не наоборот
            //ViewData["CategoryDropDown"] = CategoryDropDown;  // ViewData это словарь, [] - ключ, = - значение

            //Product product = new Product();


            // ^^^^^ Вместо кода сверху используем объект ViewModels => ProductVM ^^^^^
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _dataBase.Category.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null) 
            { 
                return View(productVM);
            }
            else
            {
                productVM.Product = _dataBase.Product.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }

                return View(productVM);
            }
        }

        // операция Post возвращает данные
        [HttpPost]
        [ValidateAntiForgeryToken] // аттрибут-токен для защиты данных 
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dataBase.Category.Add(obj); // добавление записи
                _dataBase.SaveChanges();     // созранение в БД
                return RedirectToAction("Index");  // возвращаемся на страниццу со всеми записями
            }
            return View(obj);

        }
    }
}
