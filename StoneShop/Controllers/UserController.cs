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
    }
}
