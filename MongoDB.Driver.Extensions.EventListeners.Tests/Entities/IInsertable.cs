using System;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    interface IInsertable
    {
        string CreatedBy { get; set; }

        DateTime CreatedOn { get; set; }
    }
}
