using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Update;
using SozlukProject.Service.Services;

namespace SozlukProject.WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TEntityCreateDto, TEntitUpdateDto, TEntityReadDto> : ControllerBase 
        where TEntity : class
        where TEntityCreateDto : class
        where TEntitUpdateDto : class
        where TEntityReadDto : class
    {
        private readonly BaseService<TEntity, TEntityCreateDto, TEntitUpdateDto, TEntityReadDto> _baseService;

        public BaseController(BaseService<TEntity, TEntityCreateDto, TEntitUpdateDto, TEntityReadDto> baseService)
        {
            _baseService = baseService;
        }


        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] EntityListSortValues sortValues)
        {
            return Ok(await _baseService.GetSortedEntityList(sortValues));
        }

        [HttpGet("{entityId}")]
        public virtual async Task<IActionResult> Get(int entityId)
        {
            return Ok(await _baseService.GetEntityById(entityId));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TEntityCreateDto entityCreateDto)
        {
            return Ok(await _baseService.CreateEntity(entityCreateDto));
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put(TEntitUpdateDto entitypdateDto)
        {
            return Ok(_baseService.UpdateEntityDto(entitypdateDto));
        }

        [HttpDelete("{entityId}")]
        public virtual async Task<IActionResult> Delete(int entityId)
        {
            return Ok(await _baseService.DeleteEntity(entityId));
        }
    }
}
