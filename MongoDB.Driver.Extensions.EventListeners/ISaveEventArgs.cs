
namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface ISaveEventArgs
    {
        object Entity { get; set; }

        object ContextData { get; set; }
    }
}
