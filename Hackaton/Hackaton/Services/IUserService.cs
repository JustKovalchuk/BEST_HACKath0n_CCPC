using Hackaton.Models.User;

namespace Hackaton.Services
{
    public interface IUserService
    {
        Task<int> InsertAsync(UserData userData);
        Task<bool> IsUserExistsAsync(string email);
        Task<bool> IsValidPasswordAsync(string email, string password);
    }
}
