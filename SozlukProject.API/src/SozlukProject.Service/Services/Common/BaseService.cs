using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class BaseService<TEntity>  where TEntity : BaseEntity
    {
        readonly private IGenericRepository<TEntity> _repository;
        public BaseService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<int> GetById(int entityId)
        {
            return 5;
            // return await _repository.GetByIdAsync(entityId, false);
        }
    }
}
