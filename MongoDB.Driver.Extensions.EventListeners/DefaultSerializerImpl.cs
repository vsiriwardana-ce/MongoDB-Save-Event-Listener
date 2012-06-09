using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public sealed class CustomSerializer : IBsonSerializer
    {
        private readonly IBsonSerializer _serializer;
        private readonly Type _baseType;
        private readonly IDependencyResolver _dependencyResolver;
        
        public CustomSerializer(IBsonSerializer serializer, Type baseType, IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
            _baseType = baseType;
            _serializer = serializer;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            bool isOfTypeBase = _baseType.IsAssignableFrom(nominalType);
            if (isOfTypeBase)
            {
                var preSaveOrUpdateEventListeners = _dependencyResolver.GetServices<IPreSaveOrUpdateEventListener>();
                if(preSaveOrUpdateEventListeners != null)
                {
                    foreach (var eventListener in preSaveOrUpdateEventListeners)
                    {
                        if (eventListener == null)
                        {
                            continue;
                        }
                        var eventDataLoader = _dependencyResolver.GetService<IEventDataLoader>();
                        var eventData = (eventDataLoader != null) ? eventDataLoader.GetData() : null;
                        eventListener.OnPreSave(new DefaultPreSaveOrUpdateEventArgs(value, eventData));
                    }
                }
            }

            _serializer.Serialize(bsonWriter, nominalType, value, options);

            if (!isOfTypeBase)
            {
                return;
            }

            var postSaveOrUpdateEventListeners =
                _dependencyResolver.GetServices<IPostSaveOrUpdateEventListener>();
            if (postSaveOrUpdateEventListeners == null)
            {
                return;
            }

            foreach (var eventListener in postSaveOrUpdateEventListeners)
            {
                if (eventListener == null)
                {
                    continue;
                }
                var eventDataLoader = _dependencyResolver.GetService<IEventDataLoader>();
                var eventData = (eventDataLoader != null) ? eventDataLoader.GetData() : null;
                eventListener.OnPostSave(new DefaultPostSaveOrUpdateEventArgs(value, eventData));
            }
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            return Deserialize(bsonReader, nominalType, nominalType, options);
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            return _serializer.Deserialize(bsonReader, nominalType, actualType, options);
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            return _serializer.GetDocumentId(document, out id, out idNominalType, out idGenerator);
        }

        public void SetDocumentId(object document, object id)
        {

        }

        public IBsonSerializationOptions GetDefaultSerializationOptions()
        {
            return _serializer.GetDefaultSerializationOptions();
        }

        public BsonSerializationInfo GetItemSerializationInfo()
        {
            return _serializer.GetItemSerializationInfo();
        }

        public BsonSerializationInfo GetMemberSerializationInfo(string memberName)
        {
            return _serializer.GetMemberSerializationInfo(memberName);
        }
    }
}
