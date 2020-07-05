using AutoMapper;
using ChatChallenge.Core.BaseModel.BaseEntity;
using ChatChallenge.Core.BaseModelDto.BaseEntityDto;
using ChatChallenge.Services.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ChatChallenge.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TEntityDto>  : ControllerBase
            where TEntity : class, IBaseEntity
            where TEntityDto : class, IBaseEntityDto
    {
        protected readonly IEntityCRUDService<TEntity, TEntityDto> _entityCRUDService;

        public BaseController(IEntityCRUDService<TEntity, TEntityDto> entityCRUDService)
        {
            _entityCRUDService = entityCRUDService;
        }

  
        /// <summary>
        /// Get all by query options.
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var list = _entityCRUDService.Get();
            return Ok(list);
        }

        /// <summary>
        /// Get a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            TEntityDto dto = await _entityCRUDService.GetById(id);

            if (dto is null)
                return NotFound();

            return Ok(dto);
        }
        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <returns>A newly created record.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityDto entityDto)
        {
            entityDto = await _entityCRUDService.Save(entityDto);
            return CreatedAtAction(WebRequestMethods.Http.Get, new { id = entityDto.Id }, entityDto);
        }

        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] TEntityDto entityDto)
        {
            if (entityDto.Id != id)
                return BadRequest();

            entityDto = await _entityCRUDService.Update(id, entityDto);

            if (entityDto is null)
                return NotFound();

            return Ok(entityDto);
        }


        /// <summary>
        /// Deletes a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A deleted record.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            TEntityDto entityDto = await _entityCRUDService.Delete(id);

            if (entityDto is null)
                return NotFound();

            return Ok(entityDto);
        }
    }
}
