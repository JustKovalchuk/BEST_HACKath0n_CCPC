using FluentValidation;
using Hackaton.Models.User;
using Hackaton.Validation.User;
using System.ComponentModel.DataAnnotations;
namespace Hackaton.Models.User
{
    public class UserData
    {
        //public UserData()
        //{
        //    Validator = new UserSignUpValidation();
        //}
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public string CopyPassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        //public IValidator<UserData> Validator { get; }

        public override string ToString()
        {
            return $"User : {this.Name}, Surname: {this.Surname}, Email: {this.Email}, Age: {this.Age}, Password: {this.Password},Copy Password: {this.CopyPassword}";
        }
    }
}