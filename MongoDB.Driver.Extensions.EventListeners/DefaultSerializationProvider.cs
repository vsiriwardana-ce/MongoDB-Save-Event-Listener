using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public sealed class DefaultSerializationProvider : IBsonSerializationProvider
    {
        private readonly Type _baseEntityType;
        private readonly IDependencyResolver _dependencyResolver;
        
        private DefaultSerializationProvider(Type baseEntityType, IDependencyResolver dependencyResolver)
        {
            _baseEntityType = baseEntityType;
            _dependencyResolver = dependencyResolver;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return
                new CustomSerializer(new BsonDefaultSerializer().GetSerializer(type), _baseEntityType, _dependencyResolver);
        }

        public static void InitializeProvider(Type baseEntityType, IDependencyResolver dependencyResolver)
        {
            if(baseEntityType == null)
            {
                throw new ArgumentNullException("baseEntityType");
            }
            BsonSerializer.RegisterSerializationProvider(new DefaultSerializationProvider(baseEntityType, dependencyResolver));
        }

        public static void InitializeProvider<TBaseEntityType>(IDependencyResolver dependencyResolver)
        {
            InitializeProvider(typeof (TBaseEntityType), dependencyResolver);
        }
    }
}
