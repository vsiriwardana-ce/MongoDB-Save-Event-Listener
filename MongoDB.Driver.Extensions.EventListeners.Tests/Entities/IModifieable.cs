using System;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    interface IModifieable
    {
        string ModifiedBy { get; set; }

        DateTime ModifiedOn { get; set; }
    }
}