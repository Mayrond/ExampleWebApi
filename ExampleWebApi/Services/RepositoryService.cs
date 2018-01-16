using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ExampleWebApi.Models.General;
using ExampleWebApi.Services.Interfaces;
using Z.EntityFramework.Plus;

namespace ExampleWebApi.Services
{
    public class RepositoryService<TEntity> : IRepositoryService<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _dbContext;

        public RepositoryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllNoFilteredEntities()
        {
            return _dbContext.Set<TEntity>().AsNoFilter();
        }

        public void HardDelete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public TEntity AddOrUpdate(Expression<Func<TEntity, object>> e, TEntity entity)
        {
            _dbContext.Set<TEntity>().AddOrUpdate(e, entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public void Update(TEntity entity)
        {
            _dbContext.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbContext.SaveChanges();
        }
        public TEntity Find(params object[] keyValues)
        {
            return _dbContext.Set<TEntity>().Find(keyValues);
        }

    }
}