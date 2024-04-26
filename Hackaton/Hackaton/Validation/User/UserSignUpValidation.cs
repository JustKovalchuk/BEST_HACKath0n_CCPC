using FluentValidation;
using Hackaton.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Validation.User
{
    public class UserSignUpValidation : AbstractValidator<UserData>
    {
        public UserSignUpValidation()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Surname).NotEmpty();

            RuleFor(r => r.Email).NotEmpty().EmailAddress();

            RuleFor(r => r.Age).NotEmpty().GreaterThan(6);

            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
            RuleFor(r => r.CopyPassword).NotEmpty().MinimumLength(6).Equal(r => r.Password);
        }
    }
}
