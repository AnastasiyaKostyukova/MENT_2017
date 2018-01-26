using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using NLog;
using MvcMusicStore.Controllers;
using PerformanceCounterHelper;
using MvcMusicStore.Infrastructure;

namespace MvcMusicStore
{
  using System;

  public class MvcApplication : System.Web.HttpApplication
  {
    private readonly ILogger logger;

    public MvcApplication()
    {
      this.logger = LogManager.GetCurrentClassLogger();
    }
    protected void Application_Start()
    {
      var builder = new ContainerBuilder();
      builder.RegisterControllers(typeof(HomeController).Assembly);
      builder.Register(f => LogManager.GetLogger("ForControllers")).As<ILogger>();

      DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      this.logger.Info("Hey! Application started.");

      var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Nastya5");

      DateTime currDate = DateTime.Now;
      int currMinute = currDate.Minute;
      counterHelper.RawValue(Counters.GoToHome, currMinute);

      var counterHelper1 = PerformanceHelper.CreateCounterHelper<Counters>("Nastya51");
      counterHelper1.RawValue(Counters.SuccessLogin, currMinute);
      //counterHelper.RawValue(Counters.GoToLogin, currMinute);

      //using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Nastya3"))
      //{
      //  DateTime currDate = DateTime.Now;
      //  int currMinute = currDate.Minute;
      //  counterHelper.RawValue(Counters.GoToHome, currMinute);
      //  counterHelper.RawValue(Counters.GoToLogin, currMinute);
      //  //counterHelper.RawValue(Counters.SuccessLogin, 0);
      //}
    }

    protected void Application_Error()
    {
      var ex = Server.GetLastError();

      this.logger.Error(ex.Message);
      this.logger.Error(ex.ToString());
    }
  }
}
