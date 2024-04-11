using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        [Display(Name="نام کالا")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        [Display(Name = "تعداد")]
        public int QuantityInStock { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Picture{ get; set; }
    }
}
