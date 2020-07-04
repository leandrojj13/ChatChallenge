using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Model.Entities.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Model.Contexts.ChatChallenge
{
    public class ChatChallengeDbContext : BaseDbContext, IChatChallengeDbContext
    {
        public ChatChallengeDbContext(DbContextOptions<ChatChallengeDbContext> options) : base(options)
        {
        }

        #region DbSets
        public DbSet<User> Users { get; set; }

        #endregion

        #region Methods
        public DbSet<T> GetDbSet<T>() where T : class, IBaseEntity => Set<T>();

        #endregion
    }
}

