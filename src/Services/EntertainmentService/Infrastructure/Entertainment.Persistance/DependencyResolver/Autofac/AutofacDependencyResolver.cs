using Autofac;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.Repositories.BookNoteRepositories;
using Entertainment.Application.Repositories.BookRepositories;
using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.GameRepositories;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Persistance.Concretes.Repositories.BookNoteRepositories;
using Entertainment.Persistance.Concretes.Repositories.BookRepositories;
using Entertainment.Persistance.Concretes.Repositories.GameRepositories;
using Entertainment.Persistance.Concretes.Repositories.ShowRepositories;
using Entertainment.Persistance.Concretes.Services;
using Entertainment.Persistance.Concretes.Repositories;
using Entertainment.Persistance.Concretes.Repositories.Common;

namespace Entertainment.Persistance.DependencyResolver.Autofac
{
    public class AutofacDependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameReadRepository>().As<IGameReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GameWriteRepository>().As<IGameWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<DapperBaseReadRepository>().As<IDapperBaseReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DapperBaseWriteRepository>().As<IDapperBaseWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BookNoteReadRepository>().As<IBookNoteReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BookNoteWriteRepository>().As<IBookNoteWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ShowReadRepository>().As<IShowReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ShowWriteRepository>().As<IShowWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BookReadRepository>().As<IBookReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BookWriteRepository>().As<IBookWriteRepository>().InstancePerLifetimeScope();


            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<GameService>().As<IGameService>().InstancePerLifetimeScope();
            builder.RegisterType<BookNoteService>().As<IBookNoteService>().InstancePerLifetimeScope();
            builder.RegisterType<ShowService>().As<IShowService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
