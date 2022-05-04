using System.Reflection;
using Autofac;
using SportCity.Core.Interfaces;
using SportCity.Infrastructure.Data;
using SportCity.SharedKernel.Interfaces;
using MediatR;
using MediatR.Pipeline;
using SportCity.Core.Entities.CityAggregate;
using SportCity.Core.Entities.PlaygroundAggregate;
using SportCity.Infrastructure.Identity.Services;
using SportCity.Infrastructure.Logging;
using Module = Autofac.Module;

namespace SportCity.Infrastructure;

public class DefaultInfrastructureModule : Module
{
  private readonly bool _isDevelopment = false;
  private readonly List<Assembly> _assemblies = new List<Assembly>();

  public DefaultInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
  {
    _isDevelopment = isDevelopment;
    var coreAssembly = Assembly.GetAssembly(typeof(Playground));
    var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
    if (coreAssembly != null)
    {
      _assemblies.Add(coreAssembly);
    }
    if (infrastructureAssembly != null)
    {
      _assemblies.Add(infrastructureAssembly);
    }
    if (callingAssembly != null)
    {
      _assemblies.Add(callingAssembly);
    }
  }

  protected override void Load(ContainerBuilder builder)
  {
    if (_isDevelopment)
    {
      RegisterDevelopmentOnlyDependencies(builder);
    }
    else
    {
      RegisterProductionOnlyDependencies(builder);
    }
    RegisterCommonDependencies(builder);
  }

  private void RegisterCommonDependencies(ContainerBuilder builder)
  {
    builder.RegisterGeneric(typeof(EfRepository<>))
        .As(typeof(IRepository<>))
        .As(typeof(IReadRepository<>))
        .InstancePerLifetimeScope();
    builder.RegisterGeneric(typeof(LoggerAdapter<>)).As(typeof(IAppLogger<>)).InstancePerLifetimeScope();
    builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
    
    builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
    builder.RegisterType<IdentityTokenClaimService>().As<ITokenClaimsService>().InstancePerLifetimeScope();
    builder.RegisterType<AuthorizationService>().As<IAuthorizationService>().InstancePerLifetimeScope();
    builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();


    ConfigureMediatr(builder);
  }

  private void ConfigureMediatr(ContainerBuilder builder)
  {
    builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

    builder.Register<ServiceFactory>(context =>
    {
      var c = context.Resolve<IComponentContext>();
      return t => c.Resolve(t);
    });

    var mediatrOpenTypes = new[]
    {
      typeof(IRequestHandler<,>),
      typeof(IRequestExceptionHandler<,,>),
      typeof(IRequestExceptionAction<,>),
      typeof(INotificationHandler<>),
    };

    foreach (var mediatrOpenType in mediatrOpenTypes)
    {
      builder
        .RegisterAssemblyTypes(_assemblies.ToArray())
        .AsClosedTypesOf(mediatrOpenType)
        .AsImplementedInterfaces();
    }
  }
  
  private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
  {
    // TODO: Add development only services
  }

  private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
  {
    // TODO: Add production only services
  }

}
