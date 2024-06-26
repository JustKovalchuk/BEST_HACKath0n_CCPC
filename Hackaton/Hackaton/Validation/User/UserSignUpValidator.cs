﻿using FluentValidation;
using Hackaton.Models.User;

namespace Hackaton.Validation.User
{
    public class UserSignUpValidator : AbstractValidator<UserData>
    {
        public UserSignUpValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Surname).NotEmpty();

            RuleFor(r => r.Email).NotEmpty().EmailAddress();

            RuleFor(r => r.Age).NotEmpty().GreaterThan(6);

            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
            RuleFor(r => r.CopyPassword).NotEmpty().MinimumLength(6).Equal(r => r.Password);
            RuleFor(r => r.Role).NotEmpty();

        }
    }
}
