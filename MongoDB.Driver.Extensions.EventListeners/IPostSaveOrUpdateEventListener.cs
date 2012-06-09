namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IPostSaveOrUpdateEventListener
    {
        void OnPostSave(IPostSaveOrUpdateEventArgs @event);
    }
}