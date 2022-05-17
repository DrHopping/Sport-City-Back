using Autofac;
using SportCity.Core.Entities;
using SportCity.Core.Interfaces;
using SportCity.Core.Services;

namespace SportCity.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<CityService>().As<ICityService>().InstancePerLifetimeScope();
    builder.RegisterType<SportService>().As<ISportService>().InstancePerLifetimeScope();
    builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
    builder.RegisterType<PlayerService>().As<IPlayerService>().InstancePerLifetimeScope();
    builder.RegisterType<PlaygroundService>().As<IPlaygroundService>().InstancePerLifetimeScope();
  }
}
