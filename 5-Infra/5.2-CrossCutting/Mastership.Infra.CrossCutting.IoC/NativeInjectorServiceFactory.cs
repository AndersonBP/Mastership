using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.IoC
{

    public class NativeInjectorServiceFactory

    {
        private static readonly NativeInjectorServiceFactory instance = new NativeInjectorServiceFactory();
        private static IServiceCollection _serviceCollection;
        public static NativeInjectorServiceFactory Instance
        {
            get
            {
                if (_serviceCollection == null)
                {
                    _serviceCollection = new ServiceCollection();
                    BootStrapper.RegisterServices(_serviceCollection);
                }
                return instance;
            }
        }

        public IServiceProvider GetServiceProvider()
        {
            var provider = _serviceCollection.BuildServiceProvider();
            return provider;
        }

        public IServiceCollection GetCollection()
        {
            return _serviceCollection;
        }

        public T GetService<T>()
        {
            return _serviceCollection.BuildServiceProvider().GetService<T>();
        }
    }
}

