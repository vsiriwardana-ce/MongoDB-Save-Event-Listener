using System;
using System.Collections.Generic;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);

        TServiceType GetService<TServiceType>();

        IEnumerable<object> GetServices(Type serviceType);

        IEnumerable<TServiceType> GetServices<TServiceType>();
    }
}
