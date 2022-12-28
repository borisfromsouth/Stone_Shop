using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using System.Collections.Generic;

namespace StoneShop.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dataBase;

        public UserController(ApplicationDbContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Index()
        {
            IEnumerable<User> objList = _dataBase.User;
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
        public IActionResult Create(User obj)
        {
            _dataBase.User.Add(obj); // добавление записи
            _dataBase.SaveChanges();     // созранение в БД
            return RedirectToAction("Index");  // возвращаемся на страниццу со всеми записями
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0) return NotFound();

            var obj = _dataBase.User.Find(id);
            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User obj)
        {
            if (ModelState.IsValid)
            {
                _dataBase.User.Update(obj);
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var obj = _dataBase.User.Find(id);
            if (obj == null) return NotFound();

            _dataBase.User.Remove(obj);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
