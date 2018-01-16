using AutoMapper;
using ExampleWebApi.Models.General;
using ExampleWebApi.Services.Interfaces;
using ExampleWebApi.Views.General;

namespace ExampleWebApi.Views.Converters
{
    public class BaseIdToEntityConverter<TEntity> : ITypeConverter<BaseId, TEntity> where TEntity : BaseEntity
    {
        private readonly IRepositoryService<TEntity> _repositoryService;

        public BaseIdToEntityConverter(IRepositoryService<TEntity> repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public TEntity Convert(BaseId source, TEntity destination, ResolutionContext context)
        {
            return _repositoryService.Find(source.Id);
        }
    }
}