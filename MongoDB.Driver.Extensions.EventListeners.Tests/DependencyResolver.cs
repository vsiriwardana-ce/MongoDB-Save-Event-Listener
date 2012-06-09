using System;
using System.Collections.Generic;
using Autofac;

namespace MongoDB.Driver.Extensions.EventListeners.Tests
{
    public sealed class DependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _container;

        public DependencyResolver(ILifetimeScope container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var instance = _container.Resolve(enumerableServiceType);
            return (IEnumerable<object>)instance;
        }

        public TServiceType GetService<TServiceType>()
        {
            return (TServiceType)GetService(typeof(TServiceType));
        }

        public IEnumerable<TServiceType> GetServices<TServiceType>()
        {
            return (IEnumerable<TServiceType>)GetServices(typeof(TServiceType));
        }
    }
}