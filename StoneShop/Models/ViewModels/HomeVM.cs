using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StoneShop.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categorys { get; set; }

    }
}
