namespace MongoDB.Driver.Extensions.EventListeners.Tests
{
    public sealed class EventDataLoader : IEventDataLoader
    {
        public object GetData()
        {
            return new CurrentContextData
                       {
                           CurrentUserName = "Test Runner"
                       };
        }
    }
}