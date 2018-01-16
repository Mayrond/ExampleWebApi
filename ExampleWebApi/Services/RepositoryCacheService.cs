using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExampleWebApi.Infrastructure;
using ExampleWebApi.Models.General;
using ExampleWebApi.Services.Interfaces;
using DbContext = System.Data.Entity.DbContext;

namespace ExampleWebApi.Services
{
    public class RepositoryCacheService : IRepositoryCacheService
    {
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<int, BaseEntity>> _contextCache;

        public RepositoryCacheService(DbContext context)
        {
            _contextCache = new ConcurrentDictionary<Type, ConcurrentDictionary<int, BaseEntity>>();
            var entityTypes = Utils.GetTypeByParent(typeof(BaseEntity));
            entityTypes.ForEach(entityType =>
            {
                var entites = context.Set(entityType)
                .IncludeAll(entityType)
                .ToListAsync()
                .Result
                .Select(e => new KeyValuePair<int,BaseEntity>(((BaseEntity)e).Id,(BaseEntity) e));
                _contextCache.TryAdd(entityType, new ConcurrentDictionary<int, BaseEntity>(entites));
            });
        }
        
        public TEntity Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            //now work only with BaseEntity
            var id = (int)keyValues[0];
            if (!_contextCache.TryGetValue(typeof(TEntity), out ConcurrentDictionary<int, BaseEntity> entitiesDictionary))
                throw new Exception("Not found dictionary of entity");
            if(!entitiesDictionary.TryGetValue(id, out BaseEntity entity))
                throw new Exception("Not found entity in dictionary");
            return entity as TEntity;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}