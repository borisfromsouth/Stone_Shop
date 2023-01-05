using System.Collections.Generic;

namespace StoneShop.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Product>();
        }

        public User User { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
