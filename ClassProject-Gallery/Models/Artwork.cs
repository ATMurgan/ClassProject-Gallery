namespace ClassProject_Gallery.Models
{
    public class Artwork
    {
        public int ArtworkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }

        // FK
        public int ArtistId { get; set; }
        public int CategoryId { get; set; }

        // Parent ref
        public Artist Artist { get; set; }

        // Child ref
        public List<OrderItem>? OrderItem { get; set; }
        public List<Category> Categroy { get; set; }


    }
}
