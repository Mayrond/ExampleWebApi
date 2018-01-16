using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using AutoMapper;
using ExampleWebApi.Models.General;
using ExampleWebApi.Services.Interfaces;

namespace ExampleWebApi.Infrastructure
{
    public static class QueryableExtensions
    {
        public static PageResult<TOut> QueryableForOData<TSource, TOut>(
            this IQueryable<TSource> s,
            ODataQueryOptions<TSource> options,
            HttpRequestMessage request)
            where TSource : BaseEntity
        {
            var mapper = DependencyInjector.Resolve<IMapper>();
            var result = options
                .ApplyTo(s.AsNoTracking(), new ODataQuerySettings())
                .Cast<TSource>()
                .ToListFromCache()
                .Select(mapper.Map<TOut>);

            var pageResult = new PageResult<TOut>(
                result,
                request.ODataProperties().NextLink,
                request.ODataProperties().TotalCount);
            return pageResult;
        }

        public static List<TEntity> ToListFromCache<TEntity>(this IQueryable<TEntity> query)
            where TEntity : BaseEntity
        {
            var repositoryCacheService = DependencyInjector.Resolve<IRepositoryCacheService>();
            return query
                .Select(e => e.Id)
                .ToList()
                .Select(id => repositoryCacheService.Find<TEntity>(id))
                .ToList();
        }
       
        public static IQueryable IncludeAll(this IQueryable queryable, Type type) 
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }
    }
}