using System.ComponentModel.DataAnnotations;

namespace ClassProject_Gallery.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        // Child ref
        public List<Artwork> Artwork { get; set; }

    }
}
