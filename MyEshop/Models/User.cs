using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        
        
        [Required]        
        [MaxLength(50)]
        public string Email { get; set; }

        
        [Required]        
        [MaxLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
