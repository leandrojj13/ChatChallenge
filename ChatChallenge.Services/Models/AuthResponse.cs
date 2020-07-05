using ChatChallenge.Bl.Dto.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Services.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
    }
}
