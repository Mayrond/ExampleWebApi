using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using ExampleWebApi.Converters;
using ExampleWebApi.Services;
using ExampleWebApi.Services.Interfaces;

namespace ExampleWebApi.Infrastructure
{
    public static class DependencyInjector
    {
        public static IContainer Container { get; set; }
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            Container = RegisterServices(builder);
            var webApiResolver = new AutofacWebApiDependencyResolver(Container.BeginLifetimeScope());
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepositoryService<>)).InstancePerLifetimeScope();
            builder.RegisterType<Context>().InstancePerLifetimeScope();

            #region AutomapperRegistration

            //Generic converters
            builder.RegisterGeneric(typeof(BaseIdToEntityConverter<>)).AsSelf().InstancePerLifetimeScope();
            //Non generic converters
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypeConverter<,>)))
                .AsSelf().InstancePerLifetimeScope();

            builder.Register(c => new MapperConfiguration(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly())))
                .AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve<IComponentContext>().Resolve))
                .As<IMapper>().InstancePerLifetimeScope();

            #endregion

            return builder.Build();
        }
        public static ILifetimeScope BeginNewLiftimeScope()
        {
            return Container.BeginLifetimeScope();
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public static object Resolve(Type t)
        {
            return Container.Resolve(t);
        }
    }
}