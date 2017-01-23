using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using Moq;

namespace BouquetStore.WebUI.Infrastructure
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
            //Add bindings here
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Bouquet1", Price = 25 },
                new Product { Name = "Bouquet2", Price = 179 },
                new Product { Name = "Bouquet3", Price = 95 }
             });
            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}