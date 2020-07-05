using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Model.UnitOfWorks.ChatChallenge
{
 
    public class ChatChallengeUnitOfWork : IUnitOfWork<IChatChallengeDbContext>
    {
        public IChatChallengeDbContext context { get; set; }
        public readonly IServiceProvider _serviceProvider;

        public ChatChallengeUnitOfWork(IServiceProvider serviceProvider, IChatChallengeDbContext context)
        {
            _serviceProvider = serviceProvider;
            this.context = context;
        }

        public async Task<int> Commit()
        {
            var result = await context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
        {
            return (_serviceProvider.GetService(typeof(TEntity)) ?? new BaseRepository<TEntity>(this)) as IRepository<TEntity>;
        }
    }
}
