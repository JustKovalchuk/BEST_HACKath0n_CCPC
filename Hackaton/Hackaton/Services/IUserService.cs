using Hackaton.Models.User;

namespace Hackaton.Services
{
    public interface IUserService
    {
        Task<int> InsertAsync(UserData userData);
    }
}
