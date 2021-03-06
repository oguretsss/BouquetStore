﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using BouquetStore.Domain.Concrete;
using Moq;
using BouquetStore.WebUI.Abstract;
using BouquetStore.WebUI.Concrete;
using System.Configuration;

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
      kernel.Bind<IProductRepository>().To<EFProductRepository>();
      kernel.Bind<IInstagramFeed>().To<MyInstagramFeed>();

      EmailSettings emailSettings = new EmailSettings();
      kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
      .WithConstructorArgument("settings", emailSettings);
    }
  }
}