using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundAntivirus.Models

{
    public class User
    {
        [Key]
        public int Id{get; set;}

        [Required]
        public string Name{get; set;}

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(255)")]        
        public string Email{get; set;}

        [Required]
        public string Password{get; set;}
        
        [Required]
        public string Role{get; set;} //Admin, User

    }
}