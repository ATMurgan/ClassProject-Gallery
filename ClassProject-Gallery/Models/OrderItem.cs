using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClassProject_Gallery.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // FK
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ArtworkId { get; set; }

        // Parent ref
        public Order Order { get; set; }
        public Artwork Artwork { get; set; }

    }

}    
