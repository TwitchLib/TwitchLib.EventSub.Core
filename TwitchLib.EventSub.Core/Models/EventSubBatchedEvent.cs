namespace TwitchLib.EventSub.Core.Models;

public class EventSubBatchedEvent<T>
{
    public string Id { get; set; }
    public T Data { get; set; }
}