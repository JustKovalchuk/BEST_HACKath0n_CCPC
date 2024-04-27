using Hackaton.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hackaton.Models.Categories;
using Hackaton.Models.Product;
using Hackaton.Models.Chats;
using Hackaton.Models;


namespace Hackaton.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserData>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<UserData>(a => a.Sender)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserID);
        }
        
        public DbSet<UserData> UserData { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductData> ProductData { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
