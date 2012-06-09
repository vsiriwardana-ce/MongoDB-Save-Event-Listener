namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IPostSaveOrUpdateEventArgs
    {
        object Entity { get; set; }

        object Data { get; set; }
    }
}