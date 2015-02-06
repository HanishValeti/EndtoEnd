using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EndtoEnd.Repository;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace EndtoEnd
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
      System.Uri uri = new System.Uri(ConfigurationManager.AppSettings["ApiUrl"]);
      container.RegisterType<HttpClient>(
        new InjectionFactory(x =>
        new HttpClient { BaseAddress = uri }
            )
        );
      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<ISecuritiesMfRepository, SecuritiesMfRepository>();
    }
  }
}