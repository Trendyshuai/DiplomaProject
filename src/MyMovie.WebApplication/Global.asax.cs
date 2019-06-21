using Autofac;
using Autofac.Integration.Mvc;
using MyMovie.Core.RepositoryInterface;
using MyMovie.Core.ServiceInterface;
using MyMovie.Core.Services;
using MyMovie.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyMovie.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var builder = new ContainerBuilder();

            // 注入MVC
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // IOC
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EfDbContext)).InstancePerLifetimeScope();

            // User
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();

            // Movie
            builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerRequest();
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();

            // Comment
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerRequest();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerRequest();

            // Articles
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerRequest();
            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerRequest();

            // Dictionary
            builder.RegisterType<DictionaryRepository>().As<IDictionaryRepository>().InstancePerRequest();
            builder.RegisterType<DictionaryService>().As<IDictionaryService>().InstancePerRequest();

            var container = builder.Build();
            //MVC扩展
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
