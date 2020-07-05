using AutoMapper;
using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Core.BaseModelDto.BaseEntityDto;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Repositories.Generic;
using ChatChallenge.Model.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Services.Generic
{
    public class EntityCRUDService<TEntity, TEntityDto> where TEntity : class, IBaseEntity
        where TEntityDto : class, IBaseEntityDto
    {

        public IMapper _mapper { get; set; }
        public IUnitOfWork<IChatChallengeDbContext> _uow { get; set; }
        public readonly IRepository<TEntity> _repository;

        public EntityCRUDService(IMapper mapper, IUnitOfWork<IChatChallengeDbContext> uow)
        {
            _mapper = mapper;
            _uow = uow;
            _repository = _uow.GetRepository<TEntity>();
        }

        public virtual List<TEntityDto> Get()
        {
            var listDto = _mapper.Map<List<TEntityDto>>(_repository.GetAll());
            return listDto;
        }
        public virtual async Task<TEntityDto> GetById(int id)
        {
            TEntity entity = _repository.GetByIdAsNoTracking(id);

            if (entity is null)
                return null;

            TEntity result = await Task.FromResult(entity);

            TEntityDto dto = _mapper.Map<TEntityDto>(result);

            return dto;
        }
        public virtual async Task<TEntityDto> Save(TEntityDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);

            _repository.Add(entity);
            await _uow.Commit();

            entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }
        public virtual async Task<TEntityDto> Update(int id, TEntityDto entityDto)
        {
            TEntity entity = _repository.GetById(id);
            if (entity is null)
                return null;

            _mapper.Map(entityDto, entity);

            _repository.Update(entity);

            await _uow.Commit();

            entityDto = _mapper.Map(entity, entityDto);

            return entityDto;
        }
        public virtual async Task<TEntityDto> Delete(int id)
        {
            TEntity entity = _repository.GetById(id);

            if (entity is null)
                return null;

            _repository.Delete(entity);
            await _uow.Commit();

            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            return  entityDto;
        }
    }
}
