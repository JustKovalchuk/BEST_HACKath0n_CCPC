using Hackaton.Models.User;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Hackaton.Models.Categories;


namespace Hackaton.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserData> UserData { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
