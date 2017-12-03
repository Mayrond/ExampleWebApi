using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;
using AutoMapper;

namespace ExampleWebApi.Infrastructure
{
    public static class QueryableExtensions
    {
        public static PageResult<TOut> QueryableForOData<TSource, TOut>(this IQueryable<TSource> s, ODataQueryOptions<TSource> options, HttpRequestMessage request)
        {
            var resultList = new List<TOut>();
            options
                .ApplyTo(s, new ODataQuerySettings())
                .Cast<TSource>()
                .ToList()
                .ForEach(fbo => resultList.Add(DependencyInjector.Resolve<IMapper>().Map<TOut>(fbo)));
            var pageResult = new PageResult<TOut>(
                resultList,
                request.ODataProperties().NextLink,
                request.ODataProperties().TotalCount);
            return pageResult;
        }
    }
}