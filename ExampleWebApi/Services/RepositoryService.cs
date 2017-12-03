using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ExampleWebApi.Infrastructure;
using ExampleWebApi.Models.General;
using ExampleWebApi.Services.Interfaces;
using ExampleWebApi.Models;
using Z.EntityFramework.Plus;

namespace ExampleWebApi.Services
{
    public class RepositoryService<TEntity> : IRepositoryService<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _context;

        public RepositoryService(Context context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllNoFilteredEntities()
        {
            return _context.Set<TEntity>().AsNoFilter();
        }

        public void HardDelete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity AddOrUpdate(Expression<Func<TEntity, object>> e, TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(e, entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Update(TEntity entity)
        {
            _context.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            _context.SaveChanges();
        }
        public TEntity Find(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

    }
}