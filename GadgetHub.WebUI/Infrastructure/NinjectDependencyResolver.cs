using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

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
            // Mock data for testing
            Mock<IGadgetRepository> mock = new Mock<IGadgetRepository>();
            mock.Setup(m => m.Gadgets).Returns(new List<Gadget>
            {
                new Gadget { GadgetId = 1, Name = "iPhone 13", Brand = "Apple", Price = 999, Description = "Latest Apple smartphone", Category = "Smartphones" },
                new Gadget { GadgetId = 2, Name = "Galaxy S22", Brand = "Samsung", Price = 899, Description = "Samsung flagship phone", Category = "Smartphones" },
                new Gadget { GadgetId = 3, Name = "MacBook Pro", Brand = "Apple", Price = 1299, Description = "High-performance laptop", Category = "Laptops" }
            });

            kernel.Bind<IGadgetRepository>().ToConstant(mock.Object);
        }
    }
}
