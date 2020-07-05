using ChatChallenge.Bl.Dto.Chat;
using ChatChallenge.Bl.Validators.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Bl.Validators
{
    public class ChatRoomValidator : BaseValidator<ChatRoomDto>
    {
        public ChatRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Property 'name' is obligatory.");

        }
    }
}
