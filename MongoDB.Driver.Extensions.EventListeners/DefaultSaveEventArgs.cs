namespace MongoDB.Driver.Extensions.EventListeners
{
    public class DefaultSaveEventArgs : ISaveEventArgs
    {
        public object Entity { get; set; }

        public object Data { get; set; }

        public DefaultSaveEventArgs(object entity, object contextData)
        {
            Entity = entity;
            Data = contextData;
        }
    }
}