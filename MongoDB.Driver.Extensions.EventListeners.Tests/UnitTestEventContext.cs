namespace MongoDB.Driver.Extensions.EventListeners.Tests
{
    public sealed class UnitTestEventContext : IEventContext
    {
        public object Data { get; set; }
        
        public ISaveEventListener SaveEventListener { get; set; }
    }
}