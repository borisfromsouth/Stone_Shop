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
    }
}
