using Microsoft.AspNetCore.Identity;

namespace StoneShop.Models
{
    public class User : IdentityUser
    {
        // поскольку наследуемся от IdentityUser который уже имеет свои столбцы, то при миграции свойства модели добавятся к таблице 
        public string FullName { get; set; }  
    }
}
