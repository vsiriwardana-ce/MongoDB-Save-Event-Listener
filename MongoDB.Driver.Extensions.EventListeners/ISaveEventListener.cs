namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface ISaveEventListener
    {
        void OnSave(ISaveEventArgs @event);
    }
}
