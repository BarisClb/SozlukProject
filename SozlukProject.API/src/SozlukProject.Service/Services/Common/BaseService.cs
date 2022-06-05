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

        public virtual async Task<SortedResponse<IList<TEntityResponseDto>, EntityListSortValues>> GetSortedEntityList(EntityListSortValues sortValues, Expression<Func<TEntity, bool>> searchWordPredicate = null, Expression<Func<TEntity, bool>> orderByPredicate = null)
        {
            // First, we get the list. (Depending on if the User is Searching for a word or not.)
            IList<TEntity> entityList;
            if (searchWordPredicate != null)
                entityList = _genericRepository.Get(searchWordPredicate, false).ToList();
            else
                entityList = _genericRepository.Get(tracking: false).ToList();

            // Then, we apply two Sorting methods, Reverse? and OrderBy?
            if (sortValues.Reversed)
            {
                if (orderByPredicate != null)
                    entityList = entityList.AsQueryable().OrderByDescending(orderByPredicate).ToList();
                else
                    entityList = entityList.Reverse().ToList();
            }
            else
            {
                if (orderByPredicate != null)
                    entityList = entityList.AsQueryable().OrderBy(orderByPredicate).ToList();
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
            TEntity entity = await _genericRepository.GetByIdAsync(entityId);
            if (entity == null)
                return new FailResponse("Entity does not exist.");

            return new SuccessfulResponse<TEntityResponseDto>("Entity created.", _mapper.Map<TEntity, TEntityResponseDto>(entity));
        }

        public virtual async Task<SuccessfulResponse<int>> CreateEntity(TEntityCreateDto entityCreateDto)
        {
            TEntity entity = _mapper.Map<TEntityCreateDto, TEntity>(entityCreateDto);
            await _genericRepository.AddAsync(entity);
            return new SuccessfulResponse<int>("Entity updated.", entity.Id);
        }

        public virtual async Task<BaseResponse> UpdateEntity(TEntityUpdateDto entityUpdateDto)
        {
            if (await _genericRepository.GetByIdAsync(entityUpdateDto.Id, false) == null)
                return new FailResponse("Entity does not exist.");

            await _genericRepository.UpdateAsync(_mapper.Map<TEntityUpdateDto, TEntity>(entityUpdateDto));
            return new SuccessfulResponse<int>("Entity updated.", entityUpdateDto.Id);
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
