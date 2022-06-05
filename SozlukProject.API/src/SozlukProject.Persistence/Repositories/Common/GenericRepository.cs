using Microsoft.EntityFrameworkCore;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SozlukProjectDbContext _context;
        public GenericRepository(SozlukProjectDbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Table => _context.Set<TEntity>();


        // Read

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            // return await query.SingleOrDefaultAsync();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, bool tracking = true)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, bool tracking = true)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (!tracking)
                query = Table.AsNoTracking();

            return query;
        }

        // Create / Update / Delete

        public virtual int Add(TEntity entity)
        {
            Table.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
            return _context.SaveChanges();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }


        public virtual int Update(TEntity entity)
        {
            Table.Update(entity);
            _context.SaveChanges();
            return entity.Id;

            //Table.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            //_context.SaveChanges();
            //return entity.Id;
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            Table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }


        public virtual int Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached) 
                Table.Attach(entity);

            Table.Remove(entity);
            return _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                Table.Attach(entity);

            Table.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual int Delete(int id)
        {
            TEntity entity = Table.Find(id);
            return Delete(entity);
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            TEntity entity = Table.Find(id);
            return await DeleteAsync(entity);
        }

        public virtual int DeleteRange(IEnumerable<TEntity> entities)
        {
            Table.RemoveRange(entities);
            return _context.SaveChanges();

        }
        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            Table.RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual int DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
                DeleteRange(query);
            }

            return _context.SaveChanges();
        }

        public virtual async Task<int> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Table.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
                await DeleteRangeAsync(query);
            }

            return await _context.SaveChangesAsync();
        }


        public virtual int SaveChanges() =>
            _context.SaveChanges();

        public virtual Task<int> SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}
