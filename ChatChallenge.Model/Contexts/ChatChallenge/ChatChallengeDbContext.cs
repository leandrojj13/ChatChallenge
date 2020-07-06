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
                new ChatRoom { Id = 1, Name = "Room - 1", Description="Example description 1" , Picture= "https://img.icons8.com/plasticine/2x/us-dollar.png" },
                new ChatRoom { Id = 2, Name = "Room - 2", Description = "Example description 2", Picture = "https://www.iconpacks.net/icons/2/free-coin-dollar-icon-2686-thumb.png" },
                new ChatRoom { Id = 3, Name = "Room - 3", Description = "Example description 3", Picture = "https://images.vexels.com/media/users/3/130112/isolated/preview/e55cb2657abe9a4cee5fcc520df3304a-dollar-bag-circle-icon-by-vexels.png" }
            );
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}

