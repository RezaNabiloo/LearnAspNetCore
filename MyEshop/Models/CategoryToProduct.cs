using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.Models
{
    public class CategoryToProduct
    {
        [Key]
        //[Column(Order =1)]
        public int CategoryId { get; set; }
        [Key]
        //[Column(Order = 2)]
        public int ProductId { get; set; }


        //Navigation Properties
        public Category Category { get; set; }
        public Product Product{ get; set; }
    }
}
