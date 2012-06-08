
namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface ISaveEventArgs
    {
        object Entity { get; set; }

        object Data { get; set; }
    }
}
