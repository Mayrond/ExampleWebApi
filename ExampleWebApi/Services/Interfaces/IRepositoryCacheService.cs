using System.Linq;
using ExampleWebApi.Models.General;

namespace ExampleWebApi.Services.Interfaces
{
    public interface IRepositoryCacheService
    {
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity;
        TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}