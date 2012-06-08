using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.EventListeners
{
    public sealed class CustomSerializer : IBsonSerializer
    {
        private readonly DefaultSerializerOptions _options;

        public CustomSerializer(DefaultSerializerOptions options)
        {
            _options = options;
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            if (_options.Context != null)
            {
                if (_options.Context.SaveEventListener != null)
                {
                    _options.Context.SaveEventListener.OnSave(new DefaultSaveEventArgs(value, this._options.Context.Data));
                }
            }
            _options.Serializer.Serialize(bsonWriter, nominalType, value, options);
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            return Deserialize(bsonReader, nominalType, nominalType, options);
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            return _options.Serializer.Deserialize(bsonReader, nominalType, actualType, options);
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            return _options.Serializer.GetDocumentId(document, out id, out idNominalType, out idGenerator);
        }

        public void SetDocumentId(object document, object id)
        {

        }

        public IBsonSerializationOptions GetDefaultSerializationOptions()
        {
            return _options.Serializer.GetDefaultSerializationOptions();
        }

        public BsonSerializationInfo GetItemSerializationInfo()
        {
            return _options.Serializer.GetItemSerializationInfo();
        }

        public BsonSerializationInfo GetMemberSerializationInfo(string memberName)
        {
            return _options.Serializer.GetMemberSerializationInfo(memberName);
        }
    }
}
