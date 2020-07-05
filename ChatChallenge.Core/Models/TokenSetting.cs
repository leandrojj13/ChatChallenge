using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Core.Models
{
    public class TokenSetting
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
