using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Model.Entities.Chat;
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
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }

        #endregion

        #region Methods
        public DbSet<T> GetDbSet<T>() where T : class => Set<T>();
        #endregion


        #region overrides
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>().HasData(
                new ChatRoom { Id = 1, Name = "Room - 1"},
                new ChatRoom { Id = 2, Name = "Room - 2"},
                new ChatRoom { Id = 3, Name = "Room - 3"}
            );
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}

