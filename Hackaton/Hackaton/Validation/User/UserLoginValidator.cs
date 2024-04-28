using FluentValidation;
using Hackaton.Models.User;

namespace Hackaton.Validation.User
{
    public class UserLoginValidator : AbstractValidator<LogInData>
    {
        public UserLoginValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
        }
    }
}
