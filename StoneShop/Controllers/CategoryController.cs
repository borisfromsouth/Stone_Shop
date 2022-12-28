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
            if (ModelState.IsValid)
            {
                _dataBase.Category.Add(obj); // добавление записи
                _dataBase.SaveChanges();     // созранение в БД
                return RedirectToAction("Index");  // возвращаемся на страниццу со всеми записями
            }
            return View(obj);
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0) return NotFound();

            var obj = _dataBase.Category.Find(id);
            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dataBase.Category.Update(obj);
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var obj = _dataBase.Category.Find(id);
            if (obj == null) return NotFound();

            _dataBase.Category.Remove(obj);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
