using System;

namespace MongoDB.Driver.Extensions.EventListeners
{
    internal class DefaultPreSaveOrUpdateEventArgs : EventArgs, IPreSaveOrUpdateEventArgs
    {
        public object Entity { get; set; }

        public object Data { get; set; }

        public DefaultPreSaveOrUpdateEventArgs(object entity, object contextData)
        {
            Entity = entity;
            Data = contextData;
        }
    }
}
