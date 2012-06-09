namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IPreSaveOrUpdateEventListener
    {
        void OnPreSave(IPreSaveOrUpdateEventArgs @event);
    }
}