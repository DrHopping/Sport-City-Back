using Autofac;
using SportCity.Web.Middleware;

namespace SportCity.Web;

public class DefaultWebModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ExceptionHandlerMiddleware>().InstancePerLifetimeScope();
    builder.RegisterType<TokenHandlerMiddleware>().InstancePerLifetimeScope();
    builder.RegisterType<OwningUserAccessMiddleware>().InstancePerLifetimeScope();
  }
}
