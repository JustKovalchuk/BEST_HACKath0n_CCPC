using Hackaton.Models.User;
using Microsoft.EntityFrameworkCore;


namespace Hackaton.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserData> UserData { get; set; }
    }
}
