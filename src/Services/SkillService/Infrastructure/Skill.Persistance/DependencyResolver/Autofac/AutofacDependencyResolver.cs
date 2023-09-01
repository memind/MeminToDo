
using Autofac;
using Autofac.Core;
using AutoMapper;
using Skill.Application.Abstractions.Services;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Application.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Repositories.ArtRepositories;
using Skill.Persistance.Concretes.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Services;

namespace Skill.Persistance.DependencyResolver.Autofac
{
    public class AutofacDependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArtReadRepository>().As<IArtReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ArtWriteRepository>().As<IArtWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SongReadRepository>().As<ISongReadRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SongWriteRepository>().As<ISongWriteRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SongService>().As<ISongService>().InstancePerLifetimeScope();
            builder.RegisterType<ArtService>().As<IArtService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
