using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Concrete;
using System.Configuration;



namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Bind the gadget repository
            kernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();

            // Step 4 - Email Processor binding
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(System.Configuration.ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                  .WithConstructorArgument("settings", emailSettings);
        }

    }
}

