using Microsoft.EntityFrameworkCore;

namespace Hackaton.Models.User
{
    public class UserData
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public string CopyPassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
