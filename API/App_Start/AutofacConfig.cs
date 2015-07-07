using API.Factories;
using API.Registers;
using Autofac;
using Autofac.Integration.WebApi;
using DAL.DbEntity;
using System;
using System.Reflection;
using System.Web.Http;

namespace API
{
    /// <summary>
    /// Configuration to system.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Registers dependencies for Autofac to resolve via reflection at run-time.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void RegisterDependencies()
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // Register your dependencies.
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TekTak.iLoop.UOW.UnitOfWork>().As<TekTak.iLoop.UOW.IUnitOfWork>();
            //builder.RegisterType<iLoopEntity>().InstancePerLifetimeScope();

            ServiceRegister.Register(builder);
            //RegisterJServices(builder);

            AuthorizationRegister.Register(builder);

            ValidatorRegister.Register(builder);

            // Build the container.
            var container = builder.Build();

            // Create the dependency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            FluentValidation.WebApi.FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration,
              provider => provider.ValidatorFactory = new ValidatorFactory(container));
        }

        //private static void RegisterJServices(ContainerBuilder builder)
        //{
        //    builder.RegisterType<TekTak.iLoop.Account.AccountRepository>().As<TekTak.iLoop.Account.IAccountRepository>();
        //}

    }
}
