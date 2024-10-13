using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ClassProject_Gallery.Models
{
    public class Artwork
    {
        public int ArtworkId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        // FK
        [Required]
        public int ArtistId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        // Parent ref
        public Artist Artist { get; set; }
        public Category Category { get; set; }

        // Child ref
        public List<OrderItem>? OrderItem { get; set; }


    }
}
