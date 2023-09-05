using Autofac;
using Autofac.Core;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Persistance.Concretes.Repositories;
using Workout.Persistance.Concretes.Services;
using Workout.Persistance.Concretes.UnitOfWork;
using Application.Repositories;

namespace Workout.Persistance.DependencyResolver.Autofac
{
    public class AutofacDependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<WorkoutService>().As<IWorkoutService>().InstancePerLifetimeScope();
            builder.RegisterType<ExerciseService>().As<IExerciseService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
