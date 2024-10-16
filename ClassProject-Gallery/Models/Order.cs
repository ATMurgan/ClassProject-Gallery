using System.ComponentModel.DataAnnotations;

namespace ClassProject_Gallery.Models
{
    public class Order
    {
        [Key]public int OrderId { get; set; }
        [Required]
        public string OrderDate { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public string Status { get; set; }


        // Fk
        [Required]
        public int UserId { get; set; }

        // Parent ref
        public User? User { get; set; }

        // Child ref
        public List<OrderItem>? OrderItem { get; set; }
    }
}
