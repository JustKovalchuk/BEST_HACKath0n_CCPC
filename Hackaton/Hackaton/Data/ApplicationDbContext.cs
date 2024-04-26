using Hackaton.Models.User;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Hackaton.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserData> UserData { get; set; }
    }
}
