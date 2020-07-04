using System;
using System.Collections.Generic;
using System.Text;
using ChatChallenge.Core.BaseModel.BaseEntity;

namespace ChatChallenge.Model.Entities.Security
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
    }
}
