namespace TwitchLib.EventSub.Core.Models;

public class EventSubNotificationPayload<T>
{
    public EventSubSubscription Subscription { get; set; }
    public T Event { get; set; }
}
