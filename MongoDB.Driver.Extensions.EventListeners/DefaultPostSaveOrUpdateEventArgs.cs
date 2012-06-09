using System;

namespace MongoDB.Driver.Extensions.EventListeners
{
    internal class DefaultPostSaveOrUpdateEventArgs : EventArgs, IPostSaveOrUpdateEventArgs
    {
        public object Entity { get; set; }

        public object Data { get; set; }

        public DefaultPostSaveOrUpdateEventArgs(object entity, object contextData)
        {
            Entity = entity;
            Data = contextData;
        }
    }
}