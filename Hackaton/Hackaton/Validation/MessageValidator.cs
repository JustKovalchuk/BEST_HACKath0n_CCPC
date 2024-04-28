using FluentValidation;
using Hackaton.Models.Chats;

namespace Hackaton.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(r => r.Text).NotEmpty();
            RuleFor(r => r.Username).NotEmpty();
        }
    }
}
