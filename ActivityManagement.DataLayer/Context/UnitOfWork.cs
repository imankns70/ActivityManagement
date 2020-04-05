using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ActivityManagement.DataLayer.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ActivityManagementContext _context;

        public UnitOfWork(ActivityManagementContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();

        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        //public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        //{
        //    Set<TEntity>().AddRangeAsync(entities);
        //}

        //public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        //{
        //    Set<TEntity>().RemoveRange(entities);
        //}

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            throw new NotImplementedException();
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            throw new NotImplementedException();
        }

        public void ExecuteSqlCommand(string query)
        {
            
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}