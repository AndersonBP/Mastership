using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.IoC
{

    public class ServiceFactory

    {
        private static readonly ServiceFactory instance = new ServiceFactory();
        private static IServiceCollection _serviceCollection;
        public static ServiceFactory Instance
        {
            get
            {
                if (_serviceCollection == null)
                {
                    _serviceCollection = new ServiceCollection();
                    BootStrapper.RegisterServices(_serviceCollection);
                }
                if (_serviceProvider == null)
                {
                    _serviceProvider = _serviceCollection.BuildServiceProvider();
                }
                return instance;
            }
        }

        private static IServiceProvider _serviceProvider;

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        public IServiceCollection GetCollection()
        {
            return _serviceCollection;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}

