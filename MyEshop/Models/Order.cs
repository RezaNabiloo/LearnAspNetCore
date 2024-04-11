using System.ComponentModel.DataAnnotations;

namespace MyEshop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }


        public DateTime CreateDate { get; set; }


        public bool IsFinaly { get; set; }


        public virtual User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        
    }
}
