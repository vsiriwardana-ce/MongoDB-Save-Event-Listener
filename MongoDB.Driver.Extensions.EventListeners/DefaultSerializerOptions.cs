using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public sealed class DefaultSerializerOptions
    {
        public IBsonSerializer Serializer { get; private set; }

        public IEventContext Context { get; private set; }

        public DefaultSerializerOptions(IBsonSerializer serializer, IEventContext context)
        {
            Serializer = serializer;
            Context = context;
        }
    }
}