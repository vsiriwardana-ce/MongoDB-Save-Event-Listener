using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    public abstract class Entity : IEntity
    {
        public ObjectId Id { get; set; }
    }
}