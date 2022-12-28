using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoneShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Second Name")]
        public string SecondName { get; set; }

        [Required]
        [DisplayName("Third Name")]
        public string ThirdName { get; set; }

        [Required]
        [DisplayName("Birthday")]
        public DateTime Birthday { get; set; }

        [Required]
        [DisplayName("Sex")]
        public string Sex { get; set; }

        [Required]
        [DisplayName("Role")]
        public UserStatus Role { get; set; }
    }

    public enum UserStatus
    {
        Guest = 0,
        User = 1,
        Moderator = 2,
        Admin = 3
    }
}
