using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace StoneShop.Models.ViewModels
{
    public class ProductVM
    {
        // добавляем общие свойства к ProductController => Upsert который HttpGet

        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
    }
}
