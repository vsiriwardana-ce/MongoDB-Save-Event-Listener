namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IEventContext
    {
        object Data { get; set; }

        ISaveEventListener SaveEventListener { get; set; }
    }
}
