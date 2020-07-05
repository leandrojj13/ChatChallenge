using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Core.BaseModelDto.BaseEntityDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Services.Generic
{
    public interface IEntityCRUDService<TEntity, TEntityDto> where TEntity : class, IBaseEntity
        where TEntityDto : class, IBaseEntityDto
    {
        Task<TEntityDto> GetById(int id);
        Task<TEntityDto> Save(TEntityDto entityDto);
        Task<TEntityDto> Update(int id, TEntityDto entityDto);
        Task<TEntityDto> Delete(int id);
        List<TEntityDto> Get();
    }
}
