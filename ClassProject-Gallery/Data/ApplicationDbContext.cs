using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassProject_Gallery.Models;

namespace ClassProject_Gallery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ClassProject_Gallery.Models.Artwork> Artwork { get; set; } = default!;
        public DbSet<ClassProject_Gallery.Models.Category> Category { get; set; } = default!;
        public DbSet<ClassProject_Gallery.Models.OrderItem> OrderItem { get; set; } = default!;
    }
}
