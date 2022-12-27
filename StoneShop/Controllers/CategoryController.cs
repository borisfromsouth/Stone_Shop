using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using System.Collections.Generic;

namespace StoneShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dataBase;

        public CategoryController(ApplicationDbContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _dataBase.Category;
            return View(objList);
        }

        // операция GET показывает формочку
        public IActionResult Create()
        {
            return View();
        }

        // операция Post возвращает данные
        [HttpPost]
        [ValidateAntiForgeryToken] // аттрибут-токен для защиты данных 
        public IActionResult Create(Category obj)
        {
            _dataBase.Category.Add(obj); // добавление записи
            _dataBase.SaveChanges();     // созранение в БД
            return RedirectToAction("Index");  // возвращаемся на страниццу со всеми записями
        }
    }
}
