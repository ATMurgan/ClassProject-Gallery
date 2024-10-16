using System.ComponentModel.DataAnnotations;

namespace ClassProject_Gallery.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Autobiography { get; set; }
        [Required]
        public string Website { get; set; }
        public string? Socials { get; set; }

        // FK
        [Required]
        public int UserId { get; set; }

        // Parent ref
        public User? User { get; set; }

        // Child ref
        public List<Artwork>? Artwork { get; set; }
    }
}
