using System;
using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}
