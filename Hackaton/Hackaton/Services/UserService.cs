using Hackaton.Models.User;
using Hackaton.Data;

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
    }
}
