using System.Diagnostics;

namespace TwitchLib.EventSub.Core.Models;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class EventSubTransport
{
    public string Method { get; set; }
    public string? ConduitId { get; set; }
    public string? Callback { get; set; }
    public string? SessionId { get; set; }

    private string GetDebuggerDisplay()
    {

        var transportInfo = Method switch
        {
            "webhook" => Callback,
            "websocket" => SessionId,
            "conduit" => ConduitId,
            _ => "NotImplemented",
        };
        return $"{Method} - {transportInfo}";
    }
}
