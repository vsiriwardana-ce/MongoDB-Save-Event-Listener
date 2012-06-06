using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public sealed class DefaultSerializationProvider : IBsonSerializationProvider
    {
        public IEventContext ContextData { get; private set; }

        public DefaultSerializationProvider(IEventContext contextData)
        {
            ContextData = contextData;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return
                new DefaultSerializer(new DefaultSerializerOptions(new BsonDefaultSerializer().GetSerializer(type),
                                                                   ContextData));
        }
    }
}
