using Microsoft.AspNetCore.Mvc;
using StoneShop.Data;
using StoneShop.Models;
using System.Collections.Generic;

namespace StoneShop.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _dataBase;

        public ApplicationTypeController(ApplicationDbContext dataBase)
        {
            _dataBase = dataBase;
        }


        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _dataBase.ApplicationType;
            return View(objList);
        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _dataBase.ApplicationType.Add(obj);
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dataBase.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _dataBase.ApplicationType.Update(obj);
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        ////GET - DELETE
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _dataBase.ApplicationType.Find(id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(obj);
        //}

        //POST - DELETE
        //[HttpPost]
        public IActionResult Delete(int? id)
        {
            var obj = _dataBase.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dataBase.ApplicationType.Remove(obj);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}
