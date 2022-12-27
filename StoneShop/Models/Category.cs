using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoneShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(1,int.MaxValue, ErrorMessage = "Display Order must be bigger than 0")]
        public int DisplayOrder { get; set; }

    }
}
