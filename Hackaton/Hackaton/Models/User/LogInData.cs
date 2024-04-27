using FluentValidation;
using Hackaton.Models.User;
using Hackaton.Validation.User;
using System.ComponentModel.DataAnnotations;

namespace Hackaton.Models.User
{
    public class LogInData
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}