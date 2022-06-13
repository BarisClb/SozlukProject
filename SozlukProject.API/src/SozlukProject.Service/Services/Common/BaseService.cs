using AutoMapper;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Service.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class BaseService<TEntity, TEntityCreateDto, TEntityUpdateDto, TEntityReadDto>
        where TEntity : class
        where TEntityCreateDto : class
        where TEntityUpdateDto : class
        where TEntityReadDto : class
    {
        readonly private IGenericRepository<TEntity> _genericRepository;
        readonly private IMapper _mapper;
        public BaseService(IGenericRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<SortedResponse<IList<TEntityReadDto>, EntityListSortValues>> GetSortedEntityList(EntityListSortValues sortValues, Expression<Func<TEntity, bool>>? searchWordPredicate = null, Expression<Func<TEntity, object>>? orderByKeySelector = null, Expression<Func<TEntity, bool>>? entitiesByEntityPredicate = null)
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
            // Also, lets reset the orderBy if its an invalid input
            if (orderByKeySelector == null)
                sortValues.OrderBy = "DateCreated";

            // Pagination and Mapping

            // If PageSize is 0, get the whole list. Basically, a GetAll() function.
            if (sortValues.PageSize == 0)
                sortValues.PageSize = entityList.Count;

            IList<TEntityReadDto> mappedEntityList = entityList.Skip((sortValues.PageNumber - 1) * sortValues.PageSize).Take(sortValues.PageSize).Select(entity => _mapper.Map<TEntity, TEntityReadDto>(entity)).ToList();


            return new SortedResponse<IList<TEntityReadDto>, EntityListSortValues>(mappedEntityList, sortValues);
        }

        public virtual async Task<SuccessfulResponse<TEntityReadDto>> GetEntityById(int entityId)
        {
            TEntityReadDto entityReadDto = await GetAndCheckEntityByIdDto(entityId);


            return new SuccessfulResponse<TEntityReadDto>(entityReadDto);
        }

        public virtual async Task<SuccessfulResponse<TEntityReadDto>> CreateEntity(TEntityCreateDto entityCreateDto)
        {
            // Mapping Dto to Entity
            TEntity entity = _mapper.Map<TEntityCreateDto, TEntity>(entityCreateDto);
            // Adding to Database
            await _genericRepository.AddAsync(entity);
            await _genericRepository.SaveChangesAsync();


            return new SuccessfulResponse<TEntityReadDto>("Entity created.", _mapper.Map<TEntity, TEntityReadDto>(entity));
        }

        public virtual async Task<SuccessfulResponse<TEntityReadDto>> UpdateEntity(TEntity entity)
        {
            await _genericRepository.UpdateAsync(entity);
            await _genericRepository.SaveChangesAsync();


            return new SuccessfulResponse<TEntityReadDto>("Entity updated.", _mapper.Map<TEntity, TEntityReadDto>(entity));
        }

        public virtual async Task<SuccessfulResponse<TEntityReadDto>> UpdateEntityDto(TEntityUpdateDto entityUpdateDto)
        {
            TEntity entity = _mapper.Map<TEntityUpdateDto, TEntity>(entityUpdateDto);


            return await UpdateEntity(entity);
        }

        public virtual async Task<SuccessfulResponse<int>> DeleteEntity(int entityId, string entityName = null)
        {
            TEntity entity = await GetAndCheckEntityById(entityId, entityName);

            await _genericRepository.DeleteAsync(entity);
            await _genericRepository.SaveChangesAsync();


            return new SuccessfulResponse<int>("Entity deleted.", entityId);
        }


        //// Helpers

        // This helper is for UpdateEntity methods. First step is of Updating an Entity is bringing the Entity and checking if it exists. Instead of writing it in every single Service, we will do it here.
        public virtual async Task<TEntity> GetAndCheckEntityById(int entityId, string entityName = null)
        {
            TEntity entity = await _genericRepository.GetByIdAsync(entityId, false);
            if (entity == null)
                throw new Exception($"{entityName ?? "Entity"} does not exist.");


            return entity;
        }

        public virtual async Task<TEntityReadDto> GetAndCheckEntityByIdDto(int entityId)
        {
            TEntityReadDto entityReadDto = _mapper.Map<TEntity, TEntityReadDto>(await GetAndCheckEntityById(entityId));


            return entityReadDto;
        }

        public async void DeleteEntitiesWhere(Expression<Func<TEntity, bool>> predicate)
        {
            _genericRepository.DeleteWhere(predicate);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<TEntity> GetEntityWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _genericRepository.GetSingleAsync(predicate);
        }

        public async Task<TEntityReadDto> GetEntityWhereDto(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity = await _genericRepository.GetSingleAsync(predicate);


            return _mapper.Map<TEntity, TEntityReadDto>(entity);
        }

        public IQueryable<TEntity> GetEntitiesWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _genericRepository.Get(predicate);
        }

        public IQueryable<TEntityReadDto> GetEntitiesWhereDto(Expression<Func<TEntity, bool>> predicate)
        {
            return _genericRepository.Get(predicate).Select(entity => _mapper.Map<TEntity, TEntityReadDto>(entity));
        }
    }
}
