using System;
using System.Collections.Generic;

namespace TwitchLib.EventSub.Core.Models;

public class EventSubSubscription
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Version { get; set; }
    public string Status { get; set; }
    public Dictionary<string, string> Condition { get; set; }
    public EventSubTransport Transport { get; set; }
    public bool IsBatchingEnabled { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int Cost { get; set; }
}
