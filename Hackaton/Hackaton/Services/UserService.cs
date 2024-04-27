using Hackaton.Models.User;
using Hackaton.Data;
using Microsoft.EntityFrameworkCore;

namespace Hackaton.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> InsertAsync(UserData userData)
        {
            _applicationDbContext.UserData.Add(userData);
            await _applicationDbContext.SaveChangesAsync();

            return userData.Id;
        }
        public async Task<bool> IsUserExistsAsync(string email)
        {
            return await _applicationDbContext.UserData.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsValidPasswordAsync(string email, string password)
        {
            return await _applicationDbContext.UserData.AnyAsync(u => u.Email == email && u.Password == password);
        }
    }
}
