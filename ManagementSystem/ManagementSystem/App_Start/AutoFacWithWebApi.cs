using System;
using Autofac;
using Autofac.Integration.WebApi;
using Services.Interfaces;
using Services;
using Infrastructure.Data.UOW;
using System.Web.Http;
using System.Web.Mvc;
using System.Reflection;
using Infrastructure.Data.Data;
using AutoMapper;

namespace ManagementSystem.App_Start
{
    public class AutoFacWithWebApi
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserServices>().As<IUserServices>().InstancePerRequest();
            builder.RegisterType<MovieServices>().As<IMovieServices>().InstancePerRequest();
            builder.RegisterType<DirectorServices>().As<IDirectorServices>().InstancePerRequest();
            builder.RegisterType<GenreServices>().As<IGenreServices>().InstancePerRequest();
            builder.RegisterType<HistoryServices>().As<IHistoryServices>().InstancePerRequest();
            builder.RegisterType<UOW>().InstancePerRequest();

            //注册映射
            //builder.RegisterType<movie>().InstancePerRequest();
            //builder.RegisterType<movieDirector>().InstancePerRequest();
            //builder.RegisterType<movieGenre>().InstancePerRequest();
            //builder.RegisterType<directorMovie>().InstancePerRequest();

            //builder.RegisterType<Mapper>().InstancePerRequest();


            Container = builder.Build();

            return Container;
        }
    }
}