using Hackaton.Models.Chats;
using Hackaton.Models;
using Hackaton.Models.Product;
using Hackaton.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Hackaton.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<UserData> UserData { get; set; }
        public DbSet<ProductData> ProductData { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
