
namespace MongoDB.Driver.Extensions.EventListeners
{
    public interface IPreSaveOrUpdateEventArgs
    {
        object Entity { get; set; }

        object Data { get; set; }
    }
}
