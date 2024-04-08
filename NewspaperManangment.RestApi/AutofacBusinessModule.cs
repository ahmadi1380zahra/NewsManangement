
using Autofac;
using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Infrastructure;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Services.Authors;

namespace NewspaperManangment.RestApi
{
    public class AutofacBusinessModule : Autofac.Module
    {
        private IConfiguration configuration;

        public AutofacBusinessModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            var persistentAssembly = typeof(EFUnitOfWork).Assembly;
            var serviceAssembly = typeof(AuthorAppService).Assembly;
            builder.RegisterType<EFUnitOfWork>().As<UnitOfWork>();
            builder.RegisterType<DateTimeAppService>().As<DateTimeService>();
            builder.RegisterAssemblyTypes(persistentAssembly)
           .AssignableTo<Repository>()
           .AsImplementedInterfaces()
           .InstancePerLifetimeScope();
           builder.RegisterAssemblyTypes(serviceAssembly)
                .AssignableTo<Service>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
