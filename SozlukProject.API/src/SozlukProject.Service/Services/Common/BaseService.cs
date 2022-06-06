using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Response;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class BaseService<TEntity, TEntityCreateDto , TEntityUpdateDto , TEntityResponseDto> 
        where TEntity : BaseEntity
        where TEntityCreateDto : BaseEntityCreateDto
        where TEntityUpdateDto : BaseEntityUpdateDto
        where TEntityResponseDto : BaseEntityResponseDto
    {
        readonly private IGenericRepository<TEntity> _genericRepository;
        readonly private IMapper _mapper;
        public BaseService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<SortedResponse<IList<TEntityResponseDto>, EntityListSortValues>> GetSortedEntityList(EntityListSortValues sortValues, Expression<Func<TEntity, bool>>? searchWordPredicate = null, Expression<Func<TEntity, object>>? orderByKeySelector = null, Expression<Func<TEntity, bool>>? entitiesByEntityPredicate = null)
        {
            // First of all, we prepare the list.
            IList<TEntity> entityList;

            // We can start from scratch (GetAll) or we can use the Predicate for 'GetEntitiesByEntity' method.
            if (entitiesByEntityPredicate != null)
                entityList = _genericRepository.Get(entitiesByEntityPredicate, false).ToList();
            else
                entityList = _genericRepository.Get(tracking: false).ToList();

            // Now we check if sortValues has a 'SearchWord' and if true, we sort the list that contains that word.
            if (searchWordPredicate != null)
                entityList = entityList.AsQueryable().Where(searchWordPredicate).ToList();

            // Then, we apply two Sorting methods, Reverse? and OrderBy?
            if (sortValues.Reversed)
            {
                if (orderByKeySelector != null)
                    entityList = entityList.AsQueryable().OrderByDescending(orderByKeySelector).ToList();
                else
                    entityList = entityList.Reverse().ToList();
            }
            else
            {
                if (orderByKeySelector != null)
                    entityList = entityList.AsQueryable().OrderBy(orderByKeySelector).ToList();
            }

            // Pagination and Mapping

            // If PageSize is 0, get the whole list. Basically, a GetAll() function.
            if (sortValues.PageSize == 0)
                sortValues.PageSize = entityList.Count;

            IList<TEntityResponseDto> mappedEntityList = entityList.Skip((sortValues.PageNumber - 1) * sortValues.PageSize).Take(sortValues.PageSize).Select(entity => _mapper.Map<TEntity, TEntityResponseDto>(entity)).ToList();

            return new SortedResponse<IList<TEntityResponseDto>, EntityListSortValues>(mappedEntityList, sortValues);
        }

        public virtual async Task<BaseResponse> GetEntityById(int entityId)
        {
            TEntity entity = await _genericRepository.GetByIdAsync(entityId, false);
            if (entity == null)
                return new FailResponse("Entity does not exist.");

            return new SuccessfulResponse<TEntityResponseDto>(_mapper.Map<TEntity, TEntityResponseDto>(entity));
        }

        public virtual async Task<SuccessfulResponse<TEntity>> CreateEntity(TEntityCreateDto entityCreateDto)
        {
            TEntity entity = _mapper.Map<TEntityCreateDto, TEntity>(entityCreateDto);
            await _genericRepository.AddAsync(entity);
            return new SuccessfulResponse<TEntity>("Entity updated.", entity);
        }

        public virtual async Task<BaseResponse> UpdateEntity(TEntity entity)
        {
            //if (await _genericRepository.GetByIdAsync(entityUpdateDto.Id, false) == null)
            //    return new FailResponse("Entity does not exist.");

            // await _genericRepository.UpdateAsync(_mapper.Map<TEntityUpdateDto, TEntity>(entityUpdateDto));
            await _genericRepository.UpdateAsync(entity);
            return new SuccessfulResponse<TEntity>("Entity updated.", entity);
        }

        public virtual async Task<BaseResponse> DeleteEntity(int entityId)
        {
            if (await _genericRepository.GetByIdAsync(entityId, false) == null)
                return new FailResponse("Entity does not exist.");

            await _genericRepository.DeleteAsync(entityId);
            await _genericRepository.SaveChangesAsync();
            return new SuccessfulResponse<TEntity>("Category deleted.");
        }
    }
}
