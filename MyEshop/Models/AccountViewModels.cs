using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class RegisterVieModel
    {
        

        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید.")]
        [EmailAddress]
        [MaxLength(50)]
        [Remote("VerifyEmail","Account")]
        public string Email { get; set; }

        [Display(Name ="کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [MaxLength(50)]        
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }

    public class LoginVieModel
    {


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        
        public bool RememberMe{ get; set; }
    }
}
