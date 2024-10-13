using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClassProject_Gallery.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string? ProfileImage { get; set; }


        // Child ref
        public List<Artist>? Artist { get; set; }
        public List<Order>? Order { get; set; }

    }
}
