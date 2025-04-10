using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GadgetHub.WebUI.Infrastructure;
using Ninject;
using GadgetHub.WebUI.Models;
using GadgetHub.Domain.Entities;


namespace GadgetHub.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(new StandardKernel()));
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

        }
    }
}
