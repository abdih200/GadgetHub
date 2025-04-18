using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GadgetHub.WebUI.Infrastructure;
using Ninject;
using GadgetHub.WebUI.Models;
using GadgetHub.Domain.Entities;
using System.Linq;
using GadgetHub.Domain.Concrete;

namespace GadgetHub.WebUI
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(new Ninject.StandardKernel()));

            var context = new EFDbContext();
            if (!context.Gadgets.Any())
            {
                context.Gadgets.Add(new Gadget
                {
                    Name = "iPhone 14",
                    Brand = "Apple",
                    Description = "Latest Apple flagship phone",
                    Price = 999,
                    Category = "Smartphones"
                });

                context.Gadgets.Add(new Gadget
                {
                    Name = "Galaxy Watch 5",
                    Brand = "Samsung",
                    Description = "Smart wearable with fitness tracking",
                    Price = 249,
                    Category = "Wearables"
                });

                context.Gadgets.Add(new Gadget
                {
                    Name = "Surface Laptop 5",
                    Brand = "Microsoft",
                    Description = "Powerful productivity laptop",
                    Price = 1199,
                    Category = "Laptops"
                });

                context.SaveChanges();
            }
        }

        protected void Session_Start(object sender, EventArgs e) { }

        protected void Application_BeginRequest(object sender, EventArgs e) { }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }

        protected void Application_Error(object sender, EventArgs e) { }

        protected void Session_End(object sender, EventArgs e) { }

        protected void Application_End(object sender, EventArgs e) { }
    }
}
