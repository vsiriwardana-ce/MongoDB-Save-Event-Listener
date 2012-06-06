using System;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    interface IRemovable
    {
        bool IsDeleted { get; set; }

        string DeletedBy { get; set; }

        DateTime DeletedOn { get; set; }
    }
}