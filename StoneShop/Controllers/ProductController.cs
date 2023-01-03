using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoneShop.Data;
using StoneShop.Models;
using StoneShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StoneShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dataBase;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductController(ApplicationDbContext dataBase, IWebHostEnvironment webHostEnviroment)
        {
            _dataBase = dataBase;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _dataBase.Product.Include(u => u.Category).Include(u => u.ApplicationType);  // жадная загрузка (быстрая и экономная)

            ////Слишком много обращений к БД
            //IEnumerable<Product> objList = _dataBase.Product;
            //foreach (var obj in objList)
            //{
            //    obj.Category = _dataBase.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.ApplicationType = _dataBase.ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            //}

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
                }),
                ApplicationTypeSelectList = _dataBase.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
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

        
        [HttpPost]
        [ValidateAntiForgeryToken] // аттрибут-токен для защиты данных 
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;  // получаем файлы из формы ( поле имеет тип type="file" )
                string webRootPath = _webHostEnviroment.WebRootPath; // путь и папке wwwroot

                if (productVM.Product.Id == 0)
                {
                    // creating
                    string upload = webRootPath + WebConstants.ImagePath;  // путь до папки с картинками
                    string fileName = Guid.NewGuid().ToString();  // уникальный идентификатор
                    string extension = Path.GetExtension(files[0].FileName);  // расширение файла

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);  // запись в папку с картинками
                    }

                    productVM.Product.Image = fileName + extension;
                    _dataBase.Product.Add(productVM.Product);
                }
                else
                {
                    // updating
                    var objFromDb = _dataBase.Product.AsNoTracking().FirstOrDefault(U => U.Id == productVM.Product.Id);  // получаем старую запись из БД     AsNoTracking() отключает отслеживание сущности
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _dataBase.Product.Update(productVM.Product);
                }

                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }

            // при невалидности модели список категорий не возвращается, так как мы вообще передаем только текущее значение

            productVM.CategorySelectList = _dataBase.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            productVM.ApplicationTypeSelectList = _dataBase.ApplicationType.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(productVM);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var obj = _dataBase.Product.Find(id);
            if (obj == null) return NotFound();

            string webRootPath = _webHostEnviroment.WebRootPath;
            string upload = webRootPath + WebConstants.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            
            _dataBase.Product.Remove(obj);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
