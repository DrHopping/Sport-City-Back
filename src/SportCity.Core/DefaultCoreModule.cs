using Autofac;
using SportCity.Core.Entities;
using SportCity.Core.Interfaces;
using SportCity.Core.Services;

namespace SportCity.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
