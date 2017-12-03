using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ExampleWebApi.Models.General;
using ExampleWebApi.Models;

namespace ExampleWebApi.Services.Interfaces
{
    public interface IRepositoryService<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> GetAllNoFilteredEntities();
        void HardDelete(TEntity entity);
        TEntity AddOrUpdate(Expression<Func<TEntity, object>> expression, TEntity entity);
    }
}